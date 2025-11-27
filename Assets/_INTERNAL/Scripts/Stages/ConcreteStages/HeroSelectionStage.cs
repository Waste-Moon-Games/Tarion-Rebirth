using Contex.MissionInfo;
using Core.Common;
using Entry.EntryData;
using GameEntity.DataInstance.Main;
using Scripts.GameEntity.DataInstance;
using StateMachine.Base;
using UI.Base;
using UI.HeroMenu.Views;

namespace StateMachine.Stages
{
    public class HeroSelectionStage : IStage, IDisposable
    {
        private IGameStageController _controller;
        private MissionContex _missionContex;
        private AvailableHeroListView _heroListController;
        private ImperiumInstancesHolder _instanceHolder;

        public HeroSelectionStage(IGameStageController controller, StageDependencies dependencies)
        {
            _controller = controller;
            _heroListController = dependencies.UIDependencies.SelectionPanel.HeroListController;
            _instanceHolder = dependencies.InstanceHolder;
            _missionContex = dependencies.MissionContex;
        }

        public void Enter()
        {
            //_heroListController.OnHeroSelected += HandleSelectedHero;

            if (!_heroListController.gameObject.activeSelf)
                _heroListController.Show();
        }

        public void RefreshDeps(IDependence dependence)
        {
            StageDependencies currentDeps = dependence as StageDependencies;
            _heroListController = currentDeps.UIDependencies.SelectionPanel.HeroListController;
        }

        public void RefreshDeps(SimpleUIItem _) { }

        public void Tick() { }

        public void Exit()
        {
            //_heroListController.OnHeroSelected -= HandleSelectedHero;
            _heroListController.Hide();
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
            _controller.SetStage(_controller.StageFactory.CreateMissionTypeSelectionStage(_controller));
        }
    }
}