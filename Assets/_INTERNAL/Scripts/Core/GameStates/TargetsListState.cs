using Core.Common.Abstractions.GalaxyMap;
using Core.Common.Instances;
using GameEntity.DataInstance;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core.GameStates
{
    public class TargetsListState : ITargetListWriteService
    {
        private readonly List<PlanetInstance> _targets = new();

        public event Action<PlanetInstance> OnTargetAdded;
        public event Action<PlanetInstance> OnTargetRemoved;
        public event Action OnTargetsCleared;

        public IReadOnlyList<PlanetInstance> Targets => _targets.AsReadOnly();

        public void AddTarget(IInstance instance)
        {
            if (instance is not PlanetInstance planet)
                return;

            if (_targets.Contains(planet))
                return;

            _targets.Add(planet);
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