using Contex.MissionInfo;
using GameEntity.DataInstance;

namespace Core.GameStates
{
    public class ImperiumStateController
    {
        private readonly ImperiumState _state;
        private MissionContex _activeContex;

        public ImperiumStateController(ImperiumState state)
        {
            _state = state;
        }

        public void SetActiveContex(MissionContex activeContex)
        {
            Dispose();
            _activeContex = activeContex;
            _activeContex.OnPlanetCaprured += HandleCapturedPlanet;
        }

        private void Dispose()
        {
            if(_activeContex != null)
                _activeContex.OnPlanetCaprured -= HandleCapturedPlanet;

            _activeContex = null;
        }

        private void HandleCapturedPlanet(PlanetInstance capturedPlanet)
        {
            if (_state.InstanceHolder.HasAvailablePositionInPlanetsList)
            {
                _state.TargetsListState.RemoveTarget(capturedPlanet);
                _state.InstanceHolder.AddCapturedPlanet(capturedPlanet);
            }
        }
    }
}