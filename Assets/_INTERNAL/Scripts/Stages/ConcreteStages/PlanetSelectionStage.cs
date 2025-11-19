using Contex.MissionInfo;
using GameEntity.DataInstance;
using Mono.UI.PlanetListUI;
using StateMachine.Base;
using Core.Common;
using Core.GameStates;
using Entry.EntryData;

namespace StateMachine.Stages
{
    public class PlanetSelectionStage : IStage, IDisposable
    {
        private IGameStageController _controller;
        private TargetPlanetsListController _planetListController;
        private MissionContex _contex;
        private TargetsListState _targetList;

        public PlanetSelectionStage(IGameStageController controller, StageDependencies dependencies)
        {
            _controller = controller;
            _planetListController = dependencies.UIDependencies.SelectionPanel.PlanetListController;
            _contex = dependencies.MissionContex;
            _targetList = dependencies.TargetsList;
        }

        public void Enter()
        {
            _planetListController.Initialize(_targetList);
            _planetListController.OnPlanetSelected += HandleSelectedPlanet;

            if (!_planetListController.isActiveAndEnabled)
                _planetListController.Show();
        }

        public void RefreshDeps(IDependence dependence)
        {
            StageDependencies currentDeps = dependence as StageDependencies;
            _planetListController = currentDeps.UIDependencies.SelectionPanel.PlanetListController;
        }

        public void Tick() { }

        public void Exit()
        {
            _planetListController.OnPlanetSelected -= HandleSelectedPlanet;
            _planetListController.Hide();
        }

        public void Dispose()
        {
            _contex = null;
            _controller = null;
            _planetListController = null;
            _targetList = null;
        }

        private void HandleSelectedPlanet(PlanetInstance selectedPlanet)
        {
            _contex.SetPlanet(selectedPlanet);
            _controller.SetStage(_controller.StageFactory.CreateHeroSelectionStage(_controller));
        }
    }
}