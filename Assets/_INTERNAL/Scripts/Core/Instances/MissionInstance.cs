using Core.EntityDatas.Mission;
using Core.Instances.MissionPreparation;
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

        private MissionResultCalculator _calculator;
        private MissionResult _result;

        private readonly float _maxSuccessChance = 0.95f;
        private readonly float _minSuccessChance = 0.05f;

        private readonly float _minDuration = 30f;
        private readonly float _maxDuration = 300f;

        public MissionType Type => _runtimeData.Type;
        public float Difficulty => _runtimeData.Difficulty;
        public float Duration => _runtimeData.Duration;
        public int GainedExp => _runtimeData.GainedExperience;

        public MissionInstance(MissionData data, PlanetInstance planetInstance, HeroInstance heroInstance)
        {
            _runtimeData = data;
            _planetInstance = planetInstance;
            _heroInstance = heroInstance;

            _calculator = new(_minSuccessChance, _maxSuccessChance, _minDuration, _maxDuration);
        }

        public HeroInstance GetChosenHero()
        {
            return _heroInstance;
        }

        public PlanetInstance GetChonesPlanet()
        {
            return _planetInstance;
        }

        public float GetPlanetPower()
        {
            float result = _planetInstance.CalculatePlanetPower();

            return result;
        }

        public float GetHeroPower()
        {
            float result = _heroInstance.CalculateHeroPower();

            return result;
        }

        public void PrepareMission()
        {
            _result = _calculator.CalculateResult(this);

            _runtimeData.Difficulty = Mathf.RoundToInt(_result.Difficult);
            _runtimeData.Duration = Mathf.RoundToInt(_result.Duration);

            _runtimeData.GainedExperience = _result.HeroExperience;
        }
    }
}