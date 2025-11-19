using Core.CommandSystem;
using Core.CommandSystem.ConcreteCommands;
using Core.Common;
using Core.Common.Instances;
using GameEntity.DataInstance.Main;
using R3;
using Scripts.GameEntity.DataInstance;

namespace Core.ConcreteBinders
{
    public class RecruitHeroSystemBinder : ISceneBinder
    {
        private readonly IController _controller;
        private readonly ImperiumInstancesHolder _instanceHolder;
        private readonly CommandProcessor _processor;
        private readonly CompositeDisposable _disposables = new();

        public RecruitHeroSystemBinder(IController controller, ImperiumInstancesHolder instanceHolder, CommandProcessor processor)
        {
            _controller = controller;
            _instanceHolder = instanceHolder;
            _processor = processor;
        }

        public void Bind()
        {
            _controller.InstanceAdded.Subscribe(HandleSelectedPlanet).AddTo(_disposables);
        }

        public void Unbind()
        {
            _disposables.Dispose();
        }

        public void Dispose()
        {
            Unbind();
        }

        private void HandleSelectedPlanet(IInstance selectedHero)
        {
            var command = new RecruitHeroCommand
                (
                selectedHero as HeroInstance,
                _instanceHolder,
                _controller
                );

            _processor.AddCommand(command);
        }
    }
}