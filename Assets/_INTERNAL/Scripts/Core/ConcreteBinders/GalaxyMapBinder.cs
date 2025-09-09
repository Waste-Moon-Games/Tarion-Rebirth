using CommandSystem;
using Core.CommandSystem;
using Core.Common;
using Core.Common.Instances;
using Core.GameStates;
using GameEntity.DataInstance;

namespace Core.ConcreteBinders
{
    public class GalaxyMapBinder : ISceneBinder
    {
        private readonly IController _controller;
        private readonly ImperiumState _imperiumState;
        private readonly CommandProcessor _processor;

        public GalaxyMapBinder(IController controller, ImperiumState imperiumState, CommandProcessor processor)
        {
            _controller = controller;
            _imperiumState = imperiumState;
            _processor = processor;
        }

        public void Bind()
        {
            _controller.OnInstanceSelected += HandleSelectedPlanet;
        }

        public void Unbind()
        {
            _controller.OnInstanceSelected -= HandleSelectedPlanet;
        }

        public void Dispose()
        {
            Unbind();
        }

        private void HandleSelectedPlanet(IInstance selectedPlanet)
        {
            var command = new AddPlanetToTargetListCommand
                (
                selectedPlanet as PlanetInstance,
                _controller,
                _imperiumState.TargetsListState
                );

            _processor.AddCommand(command);
        }
    }
}