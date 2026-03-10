using Core.Common.MVVM;
using UI.HeroMenu.ViewModels;
using UnityEngine;
using UnityEngine.UI;

namespace UI.HeroMenu.Views
{
    public class UINavigationRootView : MonoBehaviour, IView
    {
        [Header("Navigation Buttons")]
        [SerializeField] private Button _exitButton;
        [SerializeField] private Button _barracksButton;
        [SerializeField] private Button _recruitButton;

        private HeroNavigationUIViewModel _viewModel;

        private void Start()
        {
            if (_exitButton == null || _barracksButton == null || _recruitButton == null)
                return;

            _exitButton.onClick.AddListener(HandleExitButtonClick);
            _barracksButton.onClick.AddListener(HandleBarracksButtonClick);
            _recruitButton.onClick.AddListener(HandleRecruitButtonClick);
        }

        private void OnDestroy()
        {
            if (_exitButton == null || _barracksButton == null || _recruitButton == null)
                return;

            _exitButton.onClick.RemoveListener(HandleExitButtonClick);
            _barracksButton.onClick.RemoveListener(HandleBarracksButtonClick);
            _recruitButton.onClick.RemoveListener(HandleRecruitButtonClick);
        }

        public void BindViewModel(IViewModel viewModel)
        {
            _viewModel = viewModel as HeroNavigationUIViewModel;
        }

        private void HandleExitButtonClick()
        {
            _viewModel.ExitButtonAction();
        }

        private void HandleBarracksButtonClick()
        {
            _viewModel.BarracksButtonAction();
        }

        private void HandleRecruitButtonClick()
        {
            _viewModel.RecruitButtonAction();
        }
    }
}