using GameEntity.Mission;
using Scripts.GameEntity.DataInstance;
using UnityEngine;

namespace GameEntity.DataInstance
{
    public class MissionInstance
    {
        private readonly MissionData _runtimeData;

        private readonly PlanetInstance _planetInstance;
        private readonly HeroInstance _heroInstance;

        private float _successChance;
        private readonly float _maxSuccessChance = 0.95f;
        private readonly float _mixSuccesChance = 0.05f;

        private float _duration;
        private readonly float _minDuration = 30f;
        private readonly float _maxDuration = 300f;

        public float Difficulty => _runtimeData.Difficulty;
        public float Duration => _runtimeData.Duration;
        public float Success => _successChance;
        public int GainedExp => _runtimeData.GainedExperience;

        public MissionInstance(MissionData data, PlanetInstance planetInstance, HeroInstance heroInstance)
        {
            _runtimeData = data;
            _planetInstance = planetInstance;
            _heroInstance = heroInstance;
        }

        public void PrepareMission()
        {
            float planetPower = _planetInstance.CalculatePlanetPower();
            float heroPower = _heroInstance.CalculateHeroPower();

            _successChance = CalculateSuccessChance(heroPower, planetPower);
            _duration = CalculateDuration(heroPower, planetPower);

            float difficult = planetPower / heroPower;
            _runtimeData.Difficulty = Mathf.RoundToInt(difficult);
            _runtimeData.Duration = Mathf.RoundToInt(_duration);

            _runtimeData.GainedExperience = CalculateGainedExperience(difficult, _heroInstance.HeroLevel);
        }

        private int CalculateGainedExperience(float difficult, int heroLevel)
        {
            float baseExp = 50f;
            float scaling = 1.25f;
            float heroPenalty = Mathf.Clamp01((difficult - heroLevel) * 0.1f);

            float result = baseExp * Mathf.Pow(difficult, scaling) * (1f + heroPenalty);

            return Mathf.RoundToInt(result);
        }

        private float CalculateSuccessChance(float heroPower, float planetPower)
        {
            float baseChance = heroPower / (heroPower + planetPower);

            return Mathf.Clamp(baseChance, _mixSuccesChance, _maxSuccessChance);
        }

        private float CalculateDuration(float heroPower, float planetPower)
        {
            float baseDuration = 60f;
            float difficultyFactor = planetPower / heroPower;

            return Mathf.Clamp(baseDuration * difficultyFactor, _minDuration, _maxDuration);
        }
    }
}