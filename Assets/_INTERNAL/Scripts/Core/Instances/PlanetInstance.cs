using Core.Common.Instances;
using Core.EntityDatas.Resource;
using GameEntity.Planet;
using GameEntity.ScriptableObjects;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameEntity.DataInstance
{
    public class PlanetInstance : IInstance
    {
        private readonly PlanetDataContainer _baseData;
        private readonly PlanetData _sourceData;
        private readonly PlanetRuntimeData _runtimeData;
        private readonly List<ResourceExtractor> _extractors = new();

        private bool _isBusy = false;

        public PlanetRuntimeData RuntimeData => _runtimeData;
        public ResourceData DomimanteResource { get; private set; }
        public bool IsBusy => _isBusy;
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

            InitResourceExtractors(_runtimeData.Resources);
            ChooseDominantResource();
            PlanetPower = CalculatePower();
        }

        public PlanetInstance(PlanetData generatedSourceData)
        {
            _sourceData = generatedSourceData;
            _runtimeData = new(_sourceData);

            InitResourceExtractors(_runtimeData.Resources);
            ChooseDominantResource();
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
                _runtimeData.ResistanceMultiplier *= 1.05f;
        }

        public void SetBusyStatus(bool value)
        {
            _isBusy = value;
        }

        public IEnumerable<ResourceExtractor> GetExtractors() => _extractors;

        private void InitResourceExtractors(List<ResourceData> resources)
        {
            foreach (ResourceData resource in resources)
            {
                _extractors.Add(new(resource));
            }
        }

        private void ChooseDominantResource()
        {
            if (_runtimeData.Resources == null || _runtimeData.Resources.Count == 0)
                return;

            int dominaIndex = Random.Range(0, _runtimeData.Resources.Count);

            DomimanteResource = _runtimeData.Resources[dominaIndex];

            for (int i = 0; i < _runtimeData.Resources.Count; i++)
            {
                if (i != dominaIndex)
                    Mathf.RoundToInt(_runtimeData.Resources[i].BaseExtaction * 0.8f);
            }
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