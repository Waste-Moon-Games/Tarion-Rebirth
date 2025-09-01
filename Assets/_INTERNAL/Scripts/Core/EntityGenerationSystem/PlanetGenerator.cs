using Core.EntityGenerationConfigs;
using GameEntity.Planet;
using SO.Containers.Configs;
using System;
using System.Collections.Generic;
using Random = UnityEngine.Random;

namespace Core.EntityGenerateSystem
{
    public class PlanetGenerator
    {
        private readonly PlanetDescriptionGenerator _planetDescriptionGenerator;
        private readonly PlanetGenerationConfig _config;

        private readonly HashSet<string> _usedNames = new();

        public PlanetGenerator(PlanetsGenerationConfig config)
        {
            _config = config.PlanetsConfig;
            _planetDescriptionGenerator = new(config.PlanetsConfig);
        }

        /// <summary>
        /// Генерация списка планет.
        /// </summary>
        public PlanetData GeneratePlanet()
        {
            PlanetData data = new()
            {
                //Название планеты
                PlanetName = GetRandomName(),

                //Тип планеты
                Type = GetRandomPlanetType(),

                //Описание планеты
                PlanetDescription = _planetDescriptionGenerator.GenerateDescription(),

                //Популяция планеты
                Population = Random.Range(
                    _config.MinPopulation,
                    _config.MaxPopulation),

                //Сопротивление
                BaseResistance = Random.Range(
                    _config.MinBaseResistance,
                    _config.MaxBaseResistance),

                ResistanceMultiplier = Random.Range(
                    _config.MinResistanceMultiplier,
                    _config.MaxResistanceMultiplier),

                //Технологии
                BaseTechPower = Random.Range(
                    _config.MinBaseTechPower,
                    _config.MaxBaseTechPower),

                TechMultiplier = Random.Range(
                    _config.MinTechMultiplier,
                    _config.MaxTechMultiplier)
            };

            return data;
        }

        /// <summary>
        /// Генерация списка планет.
        /// </summary>
        public List<PlanetData> GeneratePlanets(int count)
        {
            ResetUsedNames();
            List<PlanetData> planets = new();
            for (int i = 0; i < count; i++)
            {
                planets.Add(GeneratePlanet());
            }
            return planets;
        }

        /// <summary>
        /// Сброс занятых имён планет
        /// </summary>
        public void ResetUsedNames()
        {
            _usedNames.Clear();
        }

        private string GetRandomName()
        {
            if (_config.NameTemplates.Count == 0)
                return "Unnamed Planet";

            List<string> shuffled = new(_config.NameTemplates);
            Shuffle(shuffled);

            foreach (var candidate in shuffled)
            {
                if (!_usedNames.Contains(candidate))
                {
                    _usedNames.Add(candidate);
                    return candidate;
                }
            }

            string fallback = $"Planet_{Guid.NewGuid().ToString("N")[..6]}";
            _usedNames.Add(fallback);
            return fallback;
        }

        private PlanetType GetRandomPlanetType()
        {
            Array values = Enum.GetValues(typeof(PlanetType));
            return (PlanetType)values.GetValue(Random.Range(0, values.Length));
        }

        private void Shuffle<T>(List<T> list)
        {
            for (int i = list.Count - 1; i > 0; i--)
            {
                int randomIndex = Random.Range(0, i + 1);
                (list[randomIndex], list[i]) = (list[i], list[randomIndex]);
            }
        }
    }
}