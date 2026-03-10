using Core.Common.MVVM;
using Core.EntityDatas.Resource;
using GameEntity.DataInstance;
using GameEntity.DataInstance.Main;
using GameEntity.ScriptableObjects;
using R3;
using SO.Containers.Configs;
using SO.Containers.GameEntity;
using System.Collections.Generic;
using UnityEngine;
using Utils.ModCoroutines;

namespace Core.GameStates
{
    public class ImperiumState : IModel
    {
        private readonly CompositeDisposable _disposables = new();
        private readonly Subject<(int, int)> _requestHerosAndPlanetsCountSignal = new();
        private readonly Subject<(int, int)> _requestMaxHerosAndPlanetsCountSignal = new();
        private readonly Subject<Dictionary<ResourceType, int>> _requestResourcesStateSignal = new();

        [field: SerializeField] public TargetsListState TargetsListState { get; private set; }
        [field: SerializeField] public ImperiumInstancesHolder InstanceHolder { get; private set; }
        [field: SerializeField] public ImperiumResourceState ImperiumResources { get; private set; }

        private readonly float _capturedResourcesMuiltiplier;
        private float _extractionTime;

        private readonly ImperiumResourceService _resourceService;

        public Observable<(int, int)> CurrentCountRequest => _requestHerosAndPlanetsCountSignal.AsObservable();
        public Observable<(int, int)> MaxCountRequest => _requestMaxHerosAndPlanetsCountSignal.AsObservable();
        public Observable<Dictionary<ResourceType, int>> ResourcesStateRequest => _requestResourcesStateSignal.AsObservable();

        public ImperiumResourceService ResourceService => _resourceService;

        public ImperiumState(
            List<HeroDataContainer> heroDatas,
            List<PlanetDataContainer> planetDatas,
            List<MissionDataContainer> missionDatas,
            RankProgressionConfig progressionConfig,
            ImperiumConfig imperiumConfig,
            Coroutines coroutines)
        {
            InstanceHolder = new(imperiumConfig);
            InstanceHolder.Initialize(heroDatas, planetDatas, missionDatas, progressionConfig);

            TargetsListState = new();
            ImperiumResources = new();
            ImperiumResources.RequestResources.Subscribe(HandleRequestedResourcesState).AddTo(_disposables);
            ImperiumResources.RequestResourcesState();

            _extractionTime = imperiumConfig.StartExtractionTime;
            _capturedResourcesMuiltiplier = imperiumConfig.CapturedResourcesMultiplier;

            _resourceService = new
                (
                InstanceHolder,
                ImperiumResources,
                _extractionTime,
                _capturedResourcesMuiltiplier,
                coroutines
                );
        }

        public void RequestCurrentInstancesCount() => _requestHerosAndPlanetsCountSignal.OnNext((InstanceHolder.Heros.Count, InstanceHolder.Planets.Count));
        public void RequestMaxInstancesCount() => _requestMaxHerosAndPlanetsCountSignal.OnNext((InstanceHolder.MaxHeros, InstanceHolder.MaxPlanets));
        public void RequestResourcesState() => ImperiumResources.RequestResourcesState();

        public void GetCapturedResources(PlanetInstance capturedPlanet)
        {
            _resourceService?.GetCapturedResources(capturedPlanet);
            RequestCurrentInstancesCount();
            RequestMaxInstancesCount();
        }

        public void Dispose() => _disposables.Dispose();

        private void HandleRequestedResourcesState(Dictionary<ResourceType, int> resources) => _requestResourcesStateSignal.OnNext(resources);
    }
}