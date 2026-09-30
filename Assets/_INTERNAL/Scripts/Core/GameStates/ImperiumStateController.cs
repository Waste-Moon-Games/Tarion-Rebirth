using Context.MissionInfo;
using GameEntity.DataInstance;
using System.Collections.Generic;

namespace Core.GameStates
{
    public class ImperiumStateController
    {
        private readonly ImperiumState _state;
        private readonly List<MissionContext> _activeContexes = new();

        public ImperiumStateController(ImperiumState state)
        {
            _state = state;
        }

        public void SetActiveContex(MissionContext activeContex)
        {
            Dispose(activeContex);
            _activeContexes.Add(activeContex);
            activeContex.OnPlanetCaptured += HandleCapturedPlanet;
        }

        private void Dispose(MissionContext activeContex)
        {
            if(activeContex != null)
                activeContex.OnPlanetCaptured -= HandleCapturedPlanet;

            _activeContexes.Remove(activeContex);
        }

        private void HandleCapturedPlanet(PlanetInstance capturedPlanet)
        {
            if (_state.InstanceHolder.HasAvailablePositionInPlanetsList)
            {
                _state.TargetsListState.RemoveTarget(capturedPlanet);
                _state.InstanceHolder.AddCapturedPlanet(capturedPlanet);
                _state.GetCapturedResources(capturedPlanet);
            }
        }
    }
}