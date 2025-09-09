using Core.Common.Instances;
using GameEntity.Planet;
using GameEntity.ScriptableObjects;
using System;
using UnityEngine;

namespace GameEntity.DataInstance
{
    public class PlanetInstance : IInstance
    {
        private readonly PlanetDataContainer _baseData;
        private readonly PlanetData _sourceData;
        private readonly PlanetRuntimeData _runtimeData;

        public PlanetRuntimeData RuntimeData => _runtimeData;
        public float PlanetPower
        {
            get
            {
                return _runtimeData.PlanetPower;
            }
            private set
            {
                if (value < 0)
                    throw new System.ArgumentOutOfRangeException(nameof(_runtimeData.PlanetPower));

                _runtimeData.PlanetPower = value;
            }
        }

        public event Action<float> OnPowerChanged;

        public PlanetInstance(PlanetDataContainer container)
        {
            _baseData = container;
            _sourceData = _baseData.PlanetData;
            _runtimeData = new(_sourceData);

            PlanetPower = CalculatePower();
        }

        public PlanetInstance(PlanetData generatedSourceData)
        {
            _sourceData = generatedSourceData;
            _runtimeData = new(_sourceData);

            PlanetPower = CalculatePower();
        }

        public float CalculatePower()
        {
            int level = _runtimeData.Level;
            float planetResistance = CalculatePlanetResistance();
            float planetTechPower = CalculatePlanetTechPower();
            float rawPower = (planetTechPower + planetResistance) * level
                + 0.5f * planetTechPower * planetResistance * 0.01f;

            float result = PlanetPower = Mathf.RoundToInt(Mathf.Sqrt(rawPower));

            OnPowerChanged?.Invoke(result);
            return result;
        }

        public void SetPlanetStatus(bool status)
        {
            _runtimeData.IsCaptured = status;
            if (!status)
                _runtimeData.ResistanceMultiplier *= 1.2f;
        }

        private float CalculatePlanetTechPower()
        {
            float techPower = _runtimeData.BaseTechPower * (_runtimeData.Population * _runtimeData.TechMultiplier);

            return _runtimeData.TechPower = Mathf.Sqrt(techPower);
        }

        private float CalculatePlanetResistance()
        {
            float resistance = _runtimeData.BaseResistance * (_runtimeData.Population * _runtimeData.ResistanceMultiplier);

            return _runtimeData.Resistance = Mathf.Sqrt(resistance);
        }
    }
}