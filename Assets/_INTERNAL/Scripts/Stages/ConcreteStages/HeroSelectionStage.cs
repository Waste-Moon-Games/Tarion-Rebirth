using Contex.MissionInfo;
using Core.Factories.Stage_Factory;
using GameEntity.DataInstance.Main;
using Mono.UI.HeroListUI;
using Scripts.GameEntity.DataInstance;
using StateMachine.Base;
using Core.Common;

namespace StateMachine.Stages
{
    public class HeroSelectionStage : IStage, IDisposable
    {
        private IGameStageController _controller;
        private MissionContex _missionContex;
        private HeroListController _heroListController;
        private InstanceHolder _instanceHolder;

        public HeroSelectionStage(IGameStageController controller, StageDependencies dependencies)
        {
            _controller = controller;
            _heroListController = dependencies.UIDependencies.SelectionPanel.HeroListController;
            _instanceHolder = dependencies.InstanceHolder;
            _missionContex = dependencies.MissionContex;
        }

        public void Enter()
        {
            if (!_heroListController.gameObject.activeSelf)
            {
                _heroListController.Show();
            }

            _heroListController.Initialize(_instanceHolder);
            _heroListController.OnHeroSelected += HandleSelectedHero;
        }

        public void Tick() { }

        public void Exit()
        {
            _heroListController.OnHeroSelected -= HandleSelectedHero;
        }

        public void Dispose()
        {
            _controller = null;
            _missionContex = null;
            _instanceHolder = null;
            _heroListController = null;
        }

        private void HandleSelectedHero(HeroInstance selectedHero)
        {
            _missionContex.SetHero(selectedHero);

            _heroListController.Hide();

            _controller.SetStage(_controller.StageFactory.CreateMissionTypeSelectionStage(_controller));
        }
    }
}