using Core.EntityDatas.Planet;
using Core.EntityGenerationConfigs;
using GameEntity.Planet;
using SO.Containers.Configs;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.EntityGenerateSystem
{
    public class PlanetGenerator
    {
        private readonly PlanetDescriptionGenerator _planetDescriptionGenerator;
        private readonly PlanetGenerationConfig _config;

        private readonly HashSet<string> _usedNames = new();

        private readonly int[] _quotas;

        public PlanetGenerator(PlanetsGenerationConfig config)
        {
            _config = config.PlanetsConfig;
            _planetDescriptionGenerator = new(config.PlanetsConfig);
            _quotas = new int []
            { 
                _config.LowLevelPlanetCount,
                _config.MidLevelPlanetCount,
                _config.HighLevelPlanetCount
            };
        }

        /// <summary>
        /// Генерация планеты.
        /// </summary>
        public PlanetData GeneratePlanet(int planetLevel)
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
                Population = GenerateAttributeFromLevel<int>(planetLevel, _config.PopulationRange),

                //Уровень планеты
                Level = planetLevel,

                //Сопротивление
                BaseResistance = GenerateAttributeFromLevel(planetLevel, _config.BaseResistanceRange),

                ResistanceMultiplier = GenerateAttributeFromLevel(planetLevel, _config.ResistanceMultipliersRange),

                //Технологии
                BaseTechPower = GenerateAttributeFromLevel(planetLevel, _config.BaseTechPowerRange),

                TechMultiplier = GenerateAttributeFromLevel(planetLevel, _config.TechPowerMultipliersRange)
            };

            return data;
        }

        /// <summary>
        /// Генерация списка планет.
        /// </summary>
        public List<PlanetData> GeneratePlanets(int count)
        {
            ResetUsedNames();

            List<int> levels = GetRandomLevel();

            List<PlanetData> planets = new();
            for (int i = 0; i < count; i++)
            {
                planets.Add(GeneratePlanet(levels[i]));
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

        private List<int> GetRandomLevel()
        {
            var result = new List<int>();

            for (int i = 0; i < _config.LevelRange.Count; i ++)
            {
                var range = _config.LevelRange[i];

                for (int j = 0; j < _quotas[i]; j ++)
                {
                    int level = Random.Range(range.MinLevel, range.MaxLevel + 1);
                    result.Add(level);
                }
            }

            Shuffle(result);

            return result;
        }

        private T GenerateAttributeFromLevel<T>(int level, List<PlanetAttributes<T>> attributes) where T : struct
        {
            for (int i = 0; i < _config.LevelRange.Count; i++)
            {
                if (level >= _config.LevelRange[i].MinLevel && level <= _config.LevelRange[i].MaxLevel)
                {
                    var attribute = attributes[i];
                    if (typeof(T) == typeof(int))
                    {
                        int result = Random.Range(Convert.ToInt32(attribute.MinValue), Convert.ToInt32(attribute.MaxValue));
                        return (T)(object)result;
                    }
                    if (typeof(T) == typeof(float))
                    {
                        float result = Random.Range(Convert.ToSingle(attribute.MinValue), Convert.ToSingle(attribute.MaxValue));
                        return (T)(object)result;
                    }
                }
            }
            return default;
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