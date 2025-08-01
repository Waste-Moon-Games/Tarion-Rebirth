using GameEntity.Planet;
using GameEntity.ScriptableObjects;
using UnityEngine;

namespace GameEntity.DataInstance
{
    public class PlanetInstance
    {
        [field: SerializeField] public float PlanetPower {  get; private set; }

        private readonly PlanetDataContainer _baseData;
        private readonly PlanetData _sourceData;
        private readonly PlanetRuntimeData _runtimeData;

        public PlanetRuntimeData RuntimeData => _runtimeData;

        public PlanetInstance(PlanetDataContainer container)
        {
            _baseData = container;
            _sourceData = _baseData.PlanetData;
            _runtimeData = new(_sourceData);

            PlanetPower = CalculatePlanetPower();
            Debug.Log($"Planet instance: {_runtimeData.PlanetName} is initialized");
        }

        public float CalculatePlanetPower()
        {
            float planetResistance = CalculatePlanetResistance();
            float planetTechPower = CalculatePlanetTechPower();
            float rawPower = planetTechPower * planetResistance;

            return _runtimeData.PlanetPower = Mathf.RoundToInt(Mathf.Sqrt(rawPower));
        }

        public void SetPlanetStatus(bool status)
        {
            _runtimeData.IsCaptured = status;
        }

        private float CalculatePlanetTechPower()
        {
            float techPower = _runtimeData.BaseTechPower + (_runtimeData.Population * _runtimeData.TechMultiplier);

            return _runtimeData.TechPower = Mathf.Sqrt(techPower);
        }

        private float CalculatePlanetResistance()
        {
            float resistance = _runtimeData.BaseResistance + (_runtimeData.Population * _runtimeData.ResistanceMultiplier);

            return _runtimeData.Resistance = Mathf.Sqrt(resistance);
        }
    }
}