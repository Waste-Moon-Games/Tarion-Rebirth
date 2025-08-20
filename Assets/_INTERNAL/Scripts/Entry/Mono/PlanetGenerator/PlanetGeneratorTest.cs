using Core.EntityGenerateSystem;
using GameEntity.Planet;
using SO.Containers.Configs;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Entry.Mono.PlanetGeneratorMono
{
    public class PlanetGeneratorTest : MonoBehaviour
    {
        [SerializeField] private PlanetsGenerationConfig _config;
        [SerializeField] private Button _generateNewPlanets;

        private List<PlanetData> _planets = new();
        private PlanetGenerator _generator;

        private void OnEnable()
        {
            _generateNewPlanets.onClick.AddListener(NewGenerateIteration);
        }

        private void OnDisable()
        {
            _generateNewPlanets.onClick.RemoveListener(NewGenerateIteration);
        }

        private void Start()
        {
            _generator = new(_config);
            NewGenerateIteration();
        }

        private void NewGenerateIteration()
        {
            _planets.Clear();
            _generator.ResetUsedNames();

            _planets = _generator.GeneratePlanets(_config.PlanetCount);

            foreach (var planet in _planets)
            {
                Debug.Log($"Name: {planet.PlanetName}\n Type: {planet.Type}\n Desc: {planet.PlanetDescription}\n Population: {planet.Population}\n");
            }
        }
    }
}