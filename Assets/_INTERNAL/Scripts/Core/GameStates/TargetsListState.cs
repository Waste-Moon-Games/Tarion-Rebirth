using Core.Common.Abstractions.GalaxyMap;
using Core.Common.Instances;
using GameEntity.DataInstance;
using GameEntity.DataInstance.Main;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.GameStates
{
    public class TargetsListState : ITargetListWriteService
    {
        private readonly ImperiumInstancesHolder _instanceHolder;

        private readonly List<PlanetInstance> _targets = new();

        public event Action<PlanetInstance> OnTargetAdded;
        public event Action<PlanetInstance> OnTargetRemoved;
        public event Action OnTargetsCleared;

        public IReadOnlyList<PlanetInstance> Targets => _targets.AsReadOnly();

        public TargetsListState(ImperiumInstancesHolder instanceHolder)
        {
            _instanceHolder = instanceHolder;
        }

        public void AddTarget(IInstance instance)
        {
            if (instance is not PlanetInstance planet)
                return;

            if (_targets.Contains(planet))
                return;

            _targets.Add(planet);
            Debug.Log($"Planet {planet.RuntimeData.PlanetName} added to target list!");
            OnTargetAdded?.Invoke(planet);
        }

        public void RemoveTarget(IInstance instance)
        {
            if (instance is not PlanetInstance planet)
                return;

            if (!_targets.Contains(planet))
                return;

            _targets.Remove(planet);
            OnTargetRemoved?.Invoke(planet);
        }

        public void ClearTargets()
        {
            if (_targets.Count == 0)
                return;

            _targets.Clear();
            OnTargetsCleared?.Invoke();
        }
    }
}