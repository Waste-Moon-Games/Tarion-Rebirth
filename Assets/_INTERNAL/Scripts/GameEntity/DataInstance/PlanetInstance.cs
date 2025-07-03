using GameEntity.Planet;
using GameEntity.ScriptableObjects;
using UnityEngine;

namespace GameEntity.DataInstance
{
    public class PlanetInstance
    {
        private readonly PlanetDataContainer _baseData;
        private PlanetData _runtimeData;

        public PlanetData RuntimeData => _runtimeData;

        public PlanetInstance(PlanetDataContainer container)
        {
            _baseData = container;
            _runtimeData = _baseData.PlanetData;

            Debug.Log($"Planet instance: {_runtimeData.PlanetName} is initialized");
        }

        public void CalculatePlanetPower()
        {
            float planetResistance = CalculatePlanetResistance();
            float planetTechPower = CalculatePlanetTechPower();

            _runtimeData.PlanetPower = (planetTechPower * planetResistance) / Mathf.Max(_runtimeData.Population, 1);
        }

        private float CalculatePlanetTechPower()
        {
            return _runtimeData.BaseTechPower + (_runtimeData.Population * _runtimeData.TechMultiplier);
        }

        private float CalculatePlanetResistance()
        {
            return _runtimeData.BaseResistance + (_runtimeData.Population * _runtimeData.ResistanceMultiplier);
        }
    }
}