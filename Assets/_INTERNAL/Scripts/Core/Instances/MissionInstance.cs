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

        public MissionInstance(MissionData data, PlanetInstance planetInstance, HeroInstance heroInstance)
        {
            _runtimeData = data;
            _planetInstance = planetInstance;
            _heroInstance = heroInstance;

            Debug.Log($"Mission instance: {_runtimeData.Type} is initialized");
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

            Debug.Log($"Difficulty factor: {difficultyFactor}");
            return Mathf.Clamp(baseDuration * difficultyFactor, _minDuration, _maxDuration);
        }
    }
}