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

        private ISceneBinder _sceneBinder;
        private ImperiumInstancesHolder _instanceHolder;

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
            _localInstance ??= new();
            _generator ??= new(_config);
            _commandProcessor ??= new();

            _spawner.CreatePlanetsPool(_config.PlanetCount);
            _sceneBinder = new GalaxyMapBinder
                (
                this,
                GameWorldStateMono.Instance.GameWorldState.ImperiumState,
                _commandProcessor
                );
        }

        private void Update()
        {
            _commandProcessor?.Process();
        }

        public void RemovePlanet(IInstance planet)
        {
            _spawner.RemovePlanet(planet as PlanetInstance);
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