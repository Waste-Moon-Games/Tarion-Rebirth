using Core.EntityDatas.Unit;
using Core.EntityDatas.Unit.Data;
using Core.EntityGenerationConfigs;
using GameEntity.DataInstance.Main;
using GameEntity.Unit.Data;
using SO.Containers.Configs;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.EntityGenerationSystem
{
    public class HeroGenerator
    {
        private readonly HeroGenerationConfig _config;
        private readonly ImperiumInstancesHolder _instancesHolder;

        private readonly HashSet<string> _usedNames = new();
        private readonly int[] _quotas;

        public HeroGenerator(HerosGenerationConfig config, ImperiumInstancesHolder instancesHolder)
        {
            _config = config.Config;
            _instancesHolder = instancesHolder;
        }

        /// <summary>
        /// Генерация героя
        /// </summary>
        public HeroData GenerateHero()
        {
            HeroData data = new()
            {
                BaseHealth = 100,
                Level = 1,
                Experience = 0,
                HeroRank = Rank.Recruit,
                HeroGender = GenerateRandomGender(),
                HeroQuality = GenerateRandomQuality(),
                IsUnique = false
            };
            data.Description = $"Описание скоро будет готово (WIP), качество героя: {data.HeroQuality}." +
                $"\n Пол героя: {data.HeroGender}"; // to do, сделать генерацию описания на основе других данных в HeroData
            data.Stats = GenerateStats(data.HeroQuality);
            data.Name = GenerateRandomName
                (
                data.Stats.Strength,
                data.Stats.Dexterity,
                data.Stats.Intelligence,
                data.HeroGender
                );
            data.HeroArt = _config.HeroArt;

            return data;
        }

        /// <summary>
        /// Генерация списка доступных для найма героев
        /// </summary>
        public List<HeroData> GenerateHeros(int count)
        {
            ResetUsedNames();

            List<HeroData> heroes = new();
            for (int i = 0; i < count; i++)
            {
                heroes.Add(GenerateHero());
            }

            return heroes;
        }

        /// <summary>
        /// Сброс занятых имён планет
        /// </summary>
        public void ResetUsedNames()
        {
            _usedNames.Clear();
        }

        private string GenerateRandomName(int str, int dex, int intelligence, HeroGender gender)
        {
            if (_config.MaleNameTemplates.Count == 0)
                return "Unnamed Tarionion";

            HashSet<string> existingNames = _instancesHolder.Heros
                .Select(h => h?.RuntimeData.Name ?? string.Empty)
                .Where(name => !string.IsNullOrEmpty(name))
                .ToHashSet();

            string name = null;

            if (gender == HeroGender.Male)
                name = GenerateRandomNameByGender(existingNames, _config.MaleNameTemplates);
            else if (gender == HeroGender.Female)
                name = GenerateRandomNameByGender(existingNames, _config.FemaleNameTemplates);
            
            if(string.IsNullOrEmpty(name))
            {
                var stats = new[]
                {
                    (Value: str, fallback: "Warrior"),
                    (Value: dex, fallback: "Scout"),
                    (Value: intelligence, fallback: "Stategist")
                };

                name = stats.OrderByDescending(s => s.Value).First().fallback;
            }

            return name ?? "Unnamed Tarionion";
        }

        private string GenerateRandomNameByGender(HashSet<string> existingNames, List<string> nameTemplates)
        {
            int attempts = 0;
            int maxAttempts = nameTemplates.Count * 2;

            string candidate = null;
            while (attempts < maxAttempts)
            {
                int index = Random.Range(0, nameTemplates.Count);
                string name = nameTemplates[index];

                if (!_usedNames.Contains(name) && !existingNames.Contains(name))
                {
                    candidate = name;
                    break;
                }

                attempts++;
            }

            if (candidate != null)
            {
                _usedNames.Add(candidate);
                return candidate;
            }
            return null;
        }

        private HeroStats GenerateStats(HeroQuality quality)
        {
            HeroStats stats = new()
            {
                BasePower = GenerateRandomAttribute(_config.HeroBasePower, quality),
                PowerGrowthMultiplier = GenerateRandomAttribute(_config.HeroPowerGrowthMultiplier, quality),

                Strength = GenerateRandomAttribute(_config.HeroAttributes, quality),
                Dexterity = GenerateRandomAttribute(_config.HeroAttributes, quality),
                Intelligence = GenerateRandomAttribute(_config.HeroAttributes, quality),

                StrengthMultiplier = GenerateRandomAttribute(_config.AttributesMultipliers, quality),
                DexterityMultiplier = GenerateRandomAttribute(_config.AttributesMultipliers, quality),
                IntelligenceMultiplier = GenerateRandomAttribute(_config.AttributesMultipliers, quality)
            };
            return stats;
        }

        private T GenerateRandomAttribute<T>(List<HeroAttributes<T>> attributes, HeroQuality heroQuality) where T : struct
        {
            for (int i = 0; i < _config.HeroAttributes.Count; i++)
            {
                if(heroQuality == _config.HeroAttributes[i].Quality)
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

        private HeroGender GenerateRandomGender()
        {
            Array values = Enum.GetValues(typeof(HeroGender));
            return (HeroGender)Random.Range(0, values.Length);
        }

        private HeroQuality GenerateRandomQuality()
        {
            float totalWeight = 0f;
            foreach (var attribute in _config.HeroAttributes)
            {
                totalWeight += attribute.DropChance;
            }

            float roll = Random.Range(0.01f, totalWeight);

            float currentWeight = 0f;
            foreach (var attribute in _config.HeroAttributes)
            {
                currentWeight += attribute.DropChance;
                if (roll <= currentWeight)
                    return attribute.Quality;
            }

            //Fallback
            int randomFallbackIndex = Random.Range(0, _config.HeroAttributes.Count);
            return _config.HeroAttributes[randomFallbackIndex].Quality;
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