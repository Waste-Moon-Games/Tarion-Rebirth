using Core.Common.Abstractions;
using Core.Common.Abstractions.RecruitSystem;
using Core.Common.Command;
using Scripts.GameEntity.DataInstance;
using System;

namespace Core.CommandSystem.ConcreteCommands
{
    public class RecruitHeroCommand : ICommand
    {
        private readonly HeroInstance _hero;
        private readonly IInstanceHolderWriteService _instanceHolder;
        private readonly IInstanceWriteService _recruitController;

        public RecruitHeroCommand(HeroInstance hero, IInstanceHolderWriteService holder, IInstanceWriteService recruitController)
        {
            _hero = hero ?? throw new ArgumentNullException(nameof(hero));
            _instanceHolder = holder ?? throw new ArgumentNullException(nameof(holder));
            _recruitController = recruitController ?? throw new ArgumentNullException(nameof(recruitController));
        }

        public void Execute()
        {
            _instanceHolder.AddNewInstance(_hero);
            _recruitController.RemoveInstance(_hero);
        }

        public void Undo() { }
    }
}
