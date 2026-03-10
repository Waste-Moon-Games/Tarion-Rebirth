using Contex.MissionInfo;
using Core.Common;
using Entry.EntryData;
using GameEntity.DataInstance.Main;
using R3;
using Scripts.GameEntity.DataInstance;
using StateMachine.Base;
using UI.HeroMenu.Models;
using UI.HeroMenu.ViewModels;
using UI.HeroMenu.Views;

namespace StateMachine.Stages
{
    public class HeroSelectionStage : IStage, IDisposable
    {
        private readonly CompositeDisposable _disposables = new();

        private IGameStageController _controller;
        private MissionContex _missionContex;
        private AvailableHeroListView _availableHerosListView;
        private ImperiumInstancesHolder _instanceHolder;
        private AvailableHerosViewModel _availableHerosViewModel;

        public HeroSelectionStage(IGameStageController controller, StageDependencies dependencies)
        {
            _controller = controller;
            _availableHerosListView = dependencies.UIDependencies.SelectionPanel.HeroListController;
            _instanceHolder = dependencies.InstanceHolder;
            _missionContex = dependencies.MissionContex;

            _availableHerosViewModel = new();

            var barracksModel = new HeroBarracksModel(_instanceHolder);
            _availableHerosViewModel.BindModel(barracksModel);
            _availableHerosListView.BindViewModel(_availableHerosViewModel);
        }

        public void Enter()
        {
            _availableHerosViewModel.SelectedHero.Subscribe(HandleSelectedHero).AddTo(_disposables);

            if (!_availableHerosListView.gameObject.activeSelf)
                _availableHerosListView.Show();
        }

        public void RefreshDeps(IDependence dependence)
        {
            StageDependencies currentDeps = dependence as StageDependencies;
            _availableHerosListView = currentDeps.UIDependencies.SelectionPanel.HeroListController;
        }

        public void Tick() { }

        public void Exit()
        {
            _availableHerosListView.Hide();
        }

        public void Dispose()
        {
            _disposables.Clear();
            _availableHerosViewModel.Dispose();
            _controller = null;
            _missionContex = null;
            _instanceHolder = null;
            _availableHerosListView = null;
            _availableHerosViewModel = null;
        }

        private void HandleSelectedHero(HeroInstance selectedHero)
        {
            _missionContex.SetHero(selectedHero);
            _controller.SetStage(_controller.StageFactory.CreateMissionTypeSelectionStage(_controller));
        }
    }
}