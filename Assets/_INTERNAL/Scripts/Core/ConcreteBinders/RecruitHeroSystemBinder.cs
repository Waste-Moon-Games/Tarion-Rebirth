using Core.CommandSystem;
using Core.CommandSystem.ConcreteCommands;
using Core.Common;
using Core.Common.Instances;
using GameEntity.DataInstance.Main;
using Scripts.GameEntity.DataInstance;

namespace Core.ConcreteBinders
{
    public class RecruitHeroSystemBinder : ISceneBinder
    {
        private readonly IController _controller;
        private readonly ImperiumInstancesHolder _instanceHolder;
        private readonly CommandProcessor _processor;

        public RecruitHeroSystemBinder(IController controller, ImperiumInstancesHolder instanceHolder, CommandProcessor processor)
        {
            _controller = controller;
            _instanceHolder = instanceHolder;
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