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

        private readonly MissionResultCalculator _calculator;
        private MissionResult _result;

        private float _startTime;

        private readonly float _minDifficulty = 1f;
        private readonly float _maxDifficulty = 10f;

        private readonly float _maxSuccessChance = 0.95f;
        private readonly float _minSuccessChance = 0.05f;

        private readonly float _minDuration = 10f;
        private readonly float _maxDuration = 300f;

        public bool MissionSuccessful => _result.IsMissionSuccessful;
        public bool IsCompleted => Time.time >= _startTime + Duration;
        public float SuccessChance => _result.SuccessChance;
        public MissionType Type => _runtimeData.Type;
        public float Difficulty => _runtimeData.Difficulty;
        public float Duration => _runtimeData.Duration;
        public int GainedExp => _runtimeData.GainedExperience;

        public MissionInstance(MissionData data, PlanetInstance planetInstance, HeroInstance heroInstance)
        {
            _runtimeData = data;
            _planetInstance = planetInstance;
            _heroInstance = heroInstance;

            _calculator = new
                (
                _minSuccessChance,
                _maxSuccessChance,
                _minDuration,
                _maxDuration,
                _minDifficulty,
                _maxDifficulty
                );
        }

        public void BeginMission()
        {
            _startTime = Time.time;
        }

        public HeroInstance GetChosenHero()
        {
            return _heroInstance;
        }

        public float GetPlanetPower()
        {
            float result = _planetInstance.PlanetPower;

            return result;
        }

        public float GetHeroPower()
        {
            float result = _heroInstance.HeroPower;

            return result;
        }

        public void PrepareMission()
        {
            _result = _calculator.CalculateResult(this);

            _runtimeData.Difficulty = Mathf.RoundToInt(_result.Difficult);
            _runtimeData.Duration = Mathf.RoundToInt(_result.Duration);

            if (_result.IsMissionSuccessful)
                _runtimeData.GainedExperience = _result.HeroExperience;
            else
                _runtimeData.GainedExperience = Mathf.RoundToInt(_result.HeroExperience * 0.15f);
        }
    }
}