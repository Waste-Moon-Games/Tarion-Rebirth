using GameEntity.Planet;
using GameEntity.ScriptableObjects;
using UnityEngine;

namespace GameEntity.DataInstance
{
    public class PlanetInstance
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

        public PlanetInstance(PlanetDataContainer container)
        {
            _baseData = container;
            _sourceData = _baseData.PlanetData;
            _runtimeData = new(_sourceData);

            PlanetPower = CalculatePlanetPower();
        }

        public float CalculatePlanetPower()
        {
            float planetResistance = CalculatePlanetResistance();
            float planetTechPower = CalculatePlanetTechPower();
            float rawPower = planetTechPower * planetResistance;

            return PlanetPower = Mathf.RoundToInt(Mathf.Sqrt(rawPower));
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