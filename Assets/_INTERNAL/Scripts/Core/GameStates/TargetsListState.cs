using GameEntity.DataInstance;
using GameEntity.DataInstance.Main;
using System;
using System.Collections.Generic;

namespace Core.GameStates
{
    public class TargetsListState
    {
        private readonly ImperiumInstancesHolder _instanceHolder;

        private readonly Dictionary<string, PlanetInstance> _planets = new();
        private readonly List<string> _targetPlanetsIds = new();

        public event Action<string> OnTargetAdded;
        public event Action<string> OnTargetRemoved;
        public event Action OnTargetsCleared;

        public IReadOnlyList<string> TargetPlanets => _targetPlanetsIds.AsReadOnly();

        public void RegisterPlanet(PlanetInstance planet) => _planets[planet.RuntimeData.Id] = planet;

        public PlanetInstance GetPlanet(string id) => _planets.TryGetValue(id, out var planet) ? planet : null;

        public TargetsListState(ImperiumInstancesHolder instanceHolder)
        {
            _instanceHolder = instanceHolder;
        }

        public bool TryAddTarget(string planetId)
        {
            if (!_planets.ContainsKey(planetId))
                return false;

            if (_targetPlanetsIds.Contains(planetId))
                return false;

            var planet = _planets[planetId];
            if (planet.RuntimeData.IsCaptured)
                return false;

            _targetPlanetsIds.Add(planetId);
            OnTargetAdded?.Invoke(planetId);
            return true;
        }

        public bool RemoveTarget(string planetId)
        {
            if (!_targetPlanetsIds.Remove(planetId))
                return false;

            OnTargetRemoved?.Invoke(planetId);
            return true;
        }

        public void ClearTargets()
        {
            _targetPlanetsIds.Clear();
            OnTargetsCleared?.Invoke();
        }

        public IEnumerable<PlanetInstance> GetTargetPlanets()
        {
            foreach (var id in _targetPlanetsIds)
            {
                if (_planets.TryGetValue(id, out var p))
                    yield return p;
            }
        }
    }
}