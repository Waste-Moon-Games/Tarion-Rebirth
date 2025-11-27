using Core.Common.MVVM;
using R3;
using UI.HeroMenu.ViewModels;
using UnityEngine;

namespace UI.HeroMenu.Views
{
    public class HeroRecruitView : MonoBehaviour, IView
    {
        private readonly CompositeDisposable _disposables = new();
        private RecruitHerosViewModel _viewModel;

        private void OnDestroy()
        {
            _disposables.Dispose();
        }

        public void BindViewModel(IViewModel viewModel)
        {
            _viewModel = viewModel as RecruitHerosViewModel;

            _viewModel.StateChanged.Subscribe(Toggle).AddTo(_disposables);
        }

        private void Toggle(bool state)
        {
            gameObject.SetActive(state);
        }
    }
}