using GameEntity.DataInstance;
using GameEntity.DataInstance.Main;
using GameEntity.ScriptableObjects;
using SO.Containers.Configs;
using SO.Containers.GameEntity;
using System;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

namespace Core.GameStates
{
    public class ImperiumState
    {
        [field: SerializeField] public TargetsListState TargetsListState { get; private set; }
        [field: SerializeField] public ImperiumInstancesHolder InstanceHolder { get; private set; }
        [field: SerializeField] public ImperiumResourceState ImperiumResource { get; private set; }

        private readonly float _capturedResourcesMuiltiplier;
        private float _extractionTime;

        private readonly ImperiumResourceService _resourceService;

        public ImperiumResourceService ResourceService => _resourceService;

        public ImperiumState(List<HeroDataContainer> heroDatas, List<PlanetDataContainer> planetDatas, List<MissionDataContainer> missionDatas, RankProgressionConfig progressionConfig, ImperiumConfig imperiumConfig)
        {
            InstanceHolder = new(imperiumConfig);
            InstanceHolder.Initialize(heroDatas, planetDatas, missionDatas, progressionConfig);

            TargetsListState = new();
            ImperiumResource = new();

            _extractionTime = imperiumConfig.StartExtractionTime;
            _capturedResourcesMuiltiplier = imperiumConfig.CapturedResourcesMultiplier;

            _resourceService = new
                (
                InstanceHolder,
                ImperiumResource,
                _extractionTime,
                _capturedResourcesMuiltiplier
                );
        }

        public void GetCapturedResources(PlanetInstance capturedPlanet)
        {
            _resourceService.GetCapturedResources(capturedPlanet);
        }
    }
}