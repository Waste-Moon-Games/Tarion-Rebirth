using UI.PlanetsMap.ViewModels;
using UnityEngine;
using UnityEngine.UI;

namespace UI.PlanetsMap
{
    public class GalaxyMapButtonsView : MonoBehaviour
    {
        [SerializeField] private Button _exitButton;
        [SerializeField] private Button _refreshMapButton;

        private GalaxyMapButtonsViewModel _viewModel;

        private void Start()
        {
            if (_exitButton == null && _refreshMapButton == null)
                return;

            _exitButton.onClick.AddListener(HandleExitButtonClick);
            _refreshMapButton.onClick.AddListener(HandleRefreshButtonClick);
        }

        private void OnDestroy()
        {
            _exitButton.onClick.RemoveListener(HandleExitButtonClick);
            _refreshMapButton.onClick.RemoveListener(HandleRefreshButtonClick);
        }

        public void BindViewModel(GalaxyMapButtonsViewModel viewModel)
        {
            _viewModel = viewModel;
            HandleRefreshButtonClick();
        }

        private void HandleExitButtonClick()
        {
            _viewModel.CloseMap();
        }

        private void HandleRefreshButtonClick()
        {
            _viewModel.RefreshMap();
        }
    }
}