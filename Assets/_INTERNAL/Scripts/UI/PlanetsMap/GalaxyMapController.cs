using CommandSystem;
using Core.CommandSystem;
using Core.Common;
using Core.Common.Abstractions;
using Core.Common.Abstractions.GalaxyMap;
using Core.Common.Instances;
using Core.ConcreteBinders;
using Core.EntityGenerateSystem;
using Core.Instances.GalaxyMap;
using Entry.Mono;
using GameEntity.DataInstance;
using GameEntity.DataInstance.Main;
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
            _spawner.CreatePlanetsPool(_config.PlanetCount);

            _localInstance ??= new();
            _generator ??= new(_config, imperiumInstancesHolder);
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

        private void Update()
        {
            _commandProcessor?.Process();
        }

        public void RemovePlanet(IInstance planet)
        {
            _spawner.RemovePlanet(planet as PlanetInstance);
            _galaxyMapUI.SelectedPlanetUI.Hide();
        }

        private void HandleRefreshedMap()
        {
            List<PlanetData> newPlanets = _generator.GeneratePlanets(_config.PlanetCount);

            _localInstance.GetGeneratedPlanets(newPlanets);
            var spawnedPlanets = _spawner.SpawnPlanets(_localInstance.Planets);

            _galaxyMapUI.GetNewPlanets(spawnedPlanets);
        }

        private void HandleAddedPlanet(PlanetInstance addedPlanet)
        {
            OnInstanceSelected?.Invoke(addedPlanet);
        }
    }
}