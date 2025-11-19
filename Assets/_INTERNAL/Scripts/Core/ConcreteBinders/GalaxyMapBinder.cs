using CommandSystem;
using Core.CommandSystem;
using Core.Common;
using Core.Common.Instances;
using Core.GameStates;
using GameEntity.DataInstance;
using R3;

namespace Core.ConcreteBinders
{
    public class GalaxyMapBinder : ISceneBinder
    {
        private readonly IController _controller;
        private readonly ImperiumState _imperiumState;
        private readonly CommandProcessor _processor;
        private readonly CompositeDisposable _disposables = new();

        public GalaxyMapBinder(IController controller, ImperiumState imperiumState, CommandProcessor processor)
        {
            _controller = controller;
            _imperiumState = imperiumState;
            _processor = processor;
        }

        public void Bind()
        {
            _controller.InstanceAdded.Subscribe(HandleAddedPlanet).AddTo(_disposables);
        }

        public void Unbind()
        {
            _disposables.Dispose();
        }

        public void Dispose()
        {
            Unbind();
        }

        private void HandleAddedPlanet(IInstance addedPlanet)
        {
            var command = new AddPlanetToTargetListCommand
                (
                addedPlanet as PlanetInstance,
                _controller,
                _imperiumState.TargetsListState
                );

            _processor.AddCommand(command);
        }
    }
}