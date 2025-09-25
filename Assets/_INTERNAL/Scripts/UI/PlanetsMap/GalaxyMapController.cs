using Core.CommandSystem;
using Core.Common;
using Core.Common.Instances;
using Core.ConcreteBinders;
using Core.EntityGenerateSystem;
using Core.Instances.GalaxyMap;
using Entry.Mono;
using GameEntity.DataInstance;
using GameEntity.Planet;
using SO.Containers.Configs;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI.PlanetsMap
{
    public class GalaxyMapController : MonoBehaviour, IController
    {
        [Header("Map UI")]
        [SerializeField] private GalaxyMapUI _galaxyMapUI;

        [Space(10), Header("Planet generator config")]
        [SerializeField] private PlanetsGenerationConfig _config;

        [Space(10), Header("Spawner")]
        [SerializeField] private GalaxyMapSpawner _spawner;

        private ISceneBinder _localBinder;
        private SceneBinder _sceneBinder;

        private GalaxyMapInstance _localInstance;
        private PlanetGenerator _generator;
        private CommandProcessor _commandProcessor;

        public event Action<IInstance> OnInstanceSelected;

        private void OnEnable()
        {
            _galaxyMapUI.OnMapRefreshed += HandleRefreshedMap;
            _galaxyMapUI.OnPlanetAdded += HandleAddedPlanet;
        }

        private void OnDisable()
        {
            _galaxyMapUI.OnMapRefreshed -= HandleRefreshedMap;
            _galaxyMapUI.OnPlanetAdded -= HandleAddedPlanet;
        }

        private void Awake()
        {
            var imperiumState = GameWorldStateMono.Instance.GameWorldState.ImperiumState;
            var imperiumInstancesHolder = imperiumState.InstanceHolder;
            var targetList = imperiumState.TargetsListState;
            _spawner.CreatePlanetsPool(_config.PlanetCount);

            _localInstance ??= new();
            _generator ??= new(_config, imperiumInstancesHolder, targetList);
            _commandProcessor ??= new();

            _sceneBinder ??= new();
            _localBinder ??= new GalaxyMapBinder
                (
                this,
                imperiumState,
                _commandProcessor
                );
            _sceneBinder.AddBinder(_localBinder);
        }

        private void Start()
        {
            HandleRefreshedMap();
        }

        private void Update()
        {
            _commandProcessor?.Process();
        }

        public void RemoveInstance(IInstance planet)
        {
            _spawner.RemovePlanet(planet as PlanetInstance);
            _galaxyMapUI.SelectedPlanetUI.Hide();
        }

        private void HandleRefreshedMap()
        {
            List<PlanetData> newPlanets = _generator.GeneratePlanets(_config.PlanetCount);

            _localInstance.SetGeneratedPlanets(newPlanets);
            List<PlanetMapView> spawnedPlanets = _spawner.SpawnPlanets(_localInstance.Planets);

            _galaxyMapUI.SetNewPlanets(spawnedPlanets);
        }

        private void HandleAddedPlanet(PlanetInstance addedPlanet)
        {
            OnInstanceSelected?.Invoke(addedPlanet);
        }
    }
}