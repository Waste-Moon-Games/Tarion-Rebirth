using Core.EntityDatas.Mission;
using GameEntity.DataInstance;
using UnityEngine;

namespace Core.Instances.MissionPreparation
{
    public class MissionResultCalculator
    {
        private readonly float _minSuccessChance;
        private readonly float _maxSuccessChance;

        private readonly float _minDuration;
        private readonly float _maxDuration;

        public MissionResultCalculator(float minChance, float maxChance, float minDuration, float maxDuration)
        {
            _maxSuccessChance = maxChance;
            _minSuccessChance = minChance;

            _minDuration = minDuration;
            _maxDuration = maxDuration;
        }

        public MissionResult CalculateResult(MissionInstance instance)
        {
            var result = new MissionResult();

            float heroPower = instance.GetHeroPower();
            float planetPower = instance.GetPlanetPower();
            float difficult = planetPower / heroPower;

            //Бросок кубика на успех
            result.IsMissionSuccessful = RollSuccess(heroPower, planetPower);

            //Опыт герою
            result.HeroExperience = CalculateExperience(difficult, instance.GetChosenHero().HeroLevel);

            //Сложность миссии
            result.Difficult = difficult;

            //Длительность миссии
            result.Duration = CalculateDuration(heroPower, planetPower);

            //Повреждения героя
            //result.HeroInjuries = CalculateInjuries(instance);

            //Захват планеты
            result.PlanetStatus = result.IsMissionSuccessful && instance.Type == GameEntity.Mission.MissionType.Force;

            //Награды
            //result.SetRewards(GenerateRewards(instance, result.IsMissionSuccessful));

            return result;
        }

        private float CalculateDuration(float heroPower, float planetPower)
        {
            float baseDuration = 60f;
            float difficultyFactor = planetPower / heroPower;

            return Mathf.Clamp(baseDuration * difficultyFactor, _minDuration, _maxDuration);
        }

        private bool RollSuccess(float heroPower, float planetPower)
        {
            float baseChance = heroPower / (heroPower + planetPower);
            float successChance = Mathf.Clamp(baseChance, _minSuccessChance, _maxSuccessChance);

            float roll = Random.value;

            if (roll < _minSuccessChance)
                return true; //Крит. успех
            if (roll > _maxSuccessChance)
                return false; //Крит. провал

            return roll <= successChance;
        }

        private int CalculateExperience(float difficult, int heroLevel)
        {
            float baseExp = 50f;
            float scaling = 1.25f;
            float heroPenalty = Mathf.Clamp01((difficult - heroLevel) * 0.1f);

            float result = baseExp * Mathf.Pow(difficult, scaling) * (1f + heroPenalty);

            return Mathf.RoundToInt(result);
        }
    }
}