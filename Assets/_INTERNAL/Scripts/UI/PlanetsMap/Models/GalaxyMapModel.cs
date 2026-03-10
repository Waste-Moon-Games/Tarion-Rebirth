using Core.Common;
using Core.Common.Instances;
using Core.Common.MVVM;
using Core.DI;
using Core.EntityGenerateSystem;
using Core.GameStates;
using Core.Instances.GalaxyMap;
using GameEntity.DataInstance;
using GameEntity.Planet;
using R3;
using SO.Containers.Configs;
using System.Collections.Generic;
using UnityEngine;

namespace UI.PlanetsMap.Models
{
    public class GalaxyMapModel : IModel
    {
        private readonly DIContainer _sceneContainer;

        private readonly GalaxyMapSpawner _spawner;
        private readonly PlanetsGenerationConfig _config;
        private readonly PlanetGenerator _generator;

        private readonly GalaxyMapInstance _localInstance;

        private readonly Subject<IInstance> _planetAdded = new();
        private readonly Subject<PlanetInstance> _planetSelected = new();
        private readonly Subject<List<PlanetMapView>> _mapRefreshed = new();

        public Observable<List<PlanetMapView>> MapRefreshed => _mapRefreshed.AsObservable();
        public Observable<PlanetInstance> PlanetSelected => _planetSelected.AsObservable();
        public Observable<IInstance> InstanceAdded => _planetAdded.AsObservable();

        public GalaxyMapModel(GalaxyMapSpawner spawner, PlanetsGenerationConfig config, DIContainer sceneContainer)
        {
            _spawner = spawner;
            _config = config;
            _sceneContainer = sceneContainer;

            var imperiumInstancesHolder = sceneContainer.Resolve<GameState>().ImperiumState.InstanceHolder;
            var targetList = sceneContainer.Resolve<GameState>().ImperiumState.TargetsListState;

            _generator = new(config, imperiumInstancesHolder, targetList);
            _localInstance = new();
        }

        public void Init(RectTransform spawnArea)
        {
            _spawner.SetSpawnArea(spawnArea);
            _spawner.CreatePlanetsPool(_config.PlanetCount);
        }

        public void SelectPlanet(PlanetInstance selectedPlanet)
        {
            _planetSelected.OnNext(selectedPlanet);
        }

        public void RefreshMap()
        {
            List<PlanetData> generatedPlanets = _generator.GeneratePlanets(_config.PlanetCount);
            _localInstance.SetGeneratedPlanets(generatedPlanets);

            List<PlanetMapView> spawnedViews = _spawner.SpawnPlanets(_localInstance.Planets);

            _mapRefreshed.OnNext(spawnedViews);
        }

        public void RemoveInstance(IInstance instance)
        {
            _spawner.RemovePlanet(instance as PlanetInstance);
            _planetAdded.OnNext(instance);
        }

        public void AddPlanetToTargetList(PlanetInstance planet)
        {
            var targetList = _sceneContainer.Resolve<GameState>().ImperiumState.TargetsListState;
            targetList.AddTarget(planet);
            _planetAdded.OnNext(planet);
        }
    }
}