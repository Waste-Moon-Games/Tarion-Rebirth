using Core.EntityDatas.Mission;
using GameEntity.DataInstance;
using UnityEngine;

namespace Core.Instances.MissionPreparation
{
    public class MissionResultCalculator
    {
        private readonly float _minDifficulty;
        private readonly float _maxDifficulty;

        private readonly float _minSuccessChance;
        private readonly float _maxSuccessChance;

        private readonly float _minDuration;
        private readonly float _maxDuration;

        public MissionResultCalculator(float minChance, float maxChance, float minDuration, float maxDuration, float minDifficulty, float maxDifficulty)
        {
            _minDifficulty = minDifficulty;
            _maxDifficulty = maxDifficulty;

            _maxSuccessChance = maxChance;
            _minSuccessChance = minChance;

            _minDuration = minDuration;
            _maxDuration = maxDuration;
        }

        public MissionResult CalculateResult(MissionInstance instance)
        {
            MissionResult result = new();

            float heroPower = instance.GetHeroPower();
            float planetPower = instance.GetPlanetPower();

            //Расчёт сложности и её нормализация
            float ratio = planetPower / Mathf.Max(heroPower, 1f);
            float normalized = ratio / (1f + ratio);

            float difficulty = Mathf.Lerp(_minDifficulty, _maxDifficulty, normalized);

            //Бросок кубика на успех
            result.IsMissionSuccessful = RollSuccess(heroPower, difficulty, planetPower, result);

            //Опыт герою
            result.HeroExperience = CalculateExperience(difficulty);

            //Сложность миссии
            result.Difficult = difficulty;

            //Длительность миссии
            result.Duration = CalculateDuration(heroPower, planetPower);

            //Ранение героя
            //result.HeroInjuries = CalculateInjuries(instance);

            //Захват планеты
            result.PlanetStatus = result.IsMissionSuccessful && instance.Type == GameEntity.Mission.MissionType.Force;

            //Награды
            //result.SetRewards(GenerateRewards(instance, result.IsMissionSuccessful));

            return result;
        }

        private float CalculateDuration(float heroPower, float planetPower)
        {
            float ratio = planetPower / Mathf.Max(heroPower, 1f);
            float normalized = ratio / (1f + ratio);

            float duration = Mathf.Lerp(_minDuration, _maxDuration, normalized);

            return duration;
        }

        private bool RollSuccess(float heroPower, float difficulty, float planetPower, MissionResult result)
        {
            float baseChance = heroPower / (heroPower + planetPower);

            float difficultyNormalized = (difficulty - _minDifficulty) / (_maxDifficulty - _minDifficulty);

            float successChance = Mathf.Lerp(baseChance * 0.5f, baseChance * 1.5f, 1f - difficultyNormalized);
            successChance = Mathf.Clamp(successChance, _minSuccessChance, _maxSuccessChance);

            result.SuccessChance = successChance;

            float roll = Random.value;

            if (roll < _minSuccessChance)
                return true; //Крит. успех
            if (roll > _maxSuccessChance)
                return false; //Крит. провал

            return roll <= successChance;
        }

        private int CalculateExperience(float difficult)
        {
            float baseExp = 50f;
            float scaling = 1.25f;

            return Mathf.RoundToInt(baseExp * Mathf.Pow(difficult, scaling));
        }
    }
}