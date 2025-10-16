using R3;
using UI.MainMenu;
using UnityEngine;

namespace UI.Binders.MainMenu
{
    public class UIMainMenuRootBinder : MonoBehaviour
    {
        private UIMainMenuView _view;
        private MainMenuViewModel _viewModel;

        private readonly CompositeDisposable _disposables = new();

        public void Bind(UIMainMenuView view, MainMenuViewModel viewModel)
        {
            _view = view;
            _viewModel = viewModel;

            _view.GoToMapButton.onClick.AddListener(HandleGoToMapClick);
            _view.GoToHerosButton.onClick.AddListener(HandleGoToHerosClick);
            _view.ToggleMissionPreparationButton.onClick.AddListener(HandleToggleMissionPreparationClick);

            _viewModel.Actions.Where(action => action == MainMenuActions.ToggleMissionPreparation).Subscribe(_ =>
            {
                bool newState = !_viewModel.IsMissionPreparationOpen.Value;
                _viewModel.IsMissionPreparationOpen.Value = newState;
                _view.TogglePreparationWindow();
            })
            .AddTo(_disposables);
        }

        private void OnDestroy()
        {
            _disposables.Dispose();

            _view.GoToMapButton.onClick.RemoveListener(HandleGoToMapClick);
            _view.GoToHerosButton.onClick.RemoveListener(HandleGoToHerosClick);
            _view.ToggleMissionPreparationButton.onClick.RemoveListener(HandleToggleMissionPreparationClick);

            _view.Clear();
        }

        private void HandleGoToMapClick()
        {
            _viewModel.GoToMap();
        }

        private void HandleGoToHerosClick()
        {
            _viewModel.GoToHeros();
        }

        private void HandleToggleMissionPreparationClick()
        {
            _viewModel.TogglePreparationWindow();
        }
    }
}