using CommandSystem;
using Core.CommandSystem;
using Core.Common.Abstractions;
using Core.Common.Abstractions.GalaxyMap;
using Core.EntityGenerateSystem;
using Core.Instances.GalaxyMap;
using GameEntity.DataInstance;
using GameEntity.DataInstance.Main;
using GameEntity.Planet;
using SO.Containers.Configs;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace UI.PlanetsMap
{
    public class GalaxyMapController : MonoBehaviour, IMapWriteService, ITargetListWriteService
    {
        [Header("Map UI")]
        [SerializeField] private GalaxyMapUI _galaxyMapUI;

        [Space(10), Header("Planet generator config")]
        [SerializeField] private PlanetsGenerationConfig _config;

        [Space(10), Header("Spawner")]
        [SerializeField] private GalaxyMapSpawner _spawner;

        private InstanceHolder _instanceHolder;

        private GalaxyMapInstance _localInstance;
        private PlanetGenerator _generator;
        private CommandProcessor _commandProcessor;

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
        }

        private void Update()
        {
            _commandProcessor?.Process();
        }

        public void SetInstanceHolder(InstanceHolder holder)
        {
            _instanceHolder = holder;
        }

        public void AddPlanetToTarget(PlanetInstance planet)
        {
            _instanceHolder?.AddNewPlanet(planet);
        }

        public void RemovePlanet(PlanetInstance planet)
        {
            _spawner.RemovePlanet(planet);
        }

        private void HandleRefreshedMap()
        {
            List<PlanetData> newPlanets = _generator.GeneratePlanets(_config.PlanetCount);

            _localInstance.GetGeneratedPlanets(newPlanets);
            _galaxyMapUI.GetNewPlanets(_spawner.SpawnPlanets(_localInstance.Planets));
        }

        private void HandleAddedPlanet(PlanetInstance addedPlanet)
        {
            var command = new AddPlanetToTargetListCommand(
                addedPlanet,
                (IMapWriteService)this,
                (ITargetListWriteService)this
            );

            _commandProcessor.AddCommand(command);
        }
    }
}