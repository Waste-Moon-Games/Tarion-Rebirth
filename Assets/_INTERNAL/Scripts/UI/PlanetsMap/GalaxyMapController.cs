using Core.EntityGenerateSystem;
using Core.Instances.GalaxyMap;
using GameEntity.DataInstance;
using GameEntity.Planet;
using SO.Containers.Configs;
using System.Collections.Generic;
using UnityEngine;

namespace UI.PlanetsMap
{
    public class GalaxyMapController : MonoBehaviour
    {
        [Header("Map UI")]
        [SerializeField] private GalaxyMapUI _galaxyMapUI;

        [Space(10), Header("Planet generator config")]
        [SerializeField] private PlanetsGenerationConfig _config;

        [Space(10), Header("Spawner")]
        [SerializeField] private GalaxyMapSpawner _spawner;

        private GalaxyMapInstance _localInstance;
        private PlanetGenerator _generator;

        private void OnEnable()
        {
            _galaxyMapUI.OnMapRefreshed += HandleRefreshedMap;
        }

        private void OnDisable()
        {
            _galaxyMapUI.OnMapRefreshed -= HandleRefreshedMap;
        }

        private void Awake()
        {
            _localInstance ??= new();
            _generator ??= new(_config);
        }

        private void HandleRefreshedMap()
        {
            List<PlanetData> newPlanets = _generator.GeneratePlanets(_config.PlanetCount);

            _localInstance.GetGeneratedPlanets(newPlanets);
            List<PlanetInstance> planets = _localInstance.Planets;

            _galaxyMapUI.GetNewPlanets(planets);
            _spawner.SpawnPlanets(planets);
        }
    }
}