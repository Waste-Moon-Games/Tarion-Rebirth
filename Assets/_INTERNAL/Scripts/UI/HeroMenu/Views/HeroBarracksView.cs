using Core.Common.MVVM;
using R3;
using UI.HeroMenu.ViewModels;
using UnityEngine;

namespace UI.HeroMenu.Views
{
    public class HeroBarracksView : MonoBehaviour, IView
    {
        private readonly CompositeDisposable _disposables = new();

        private HeroBarracksViewModel _viewModel;

        private void OnDestroy()
        {
            _disposables.Dispose();
        }

        public void BindViewModel(IViewModel viewModel)
        {
            _viewModel = viewModel as HeroBarracksViewModel;

            _viewModel.StateChanged.Subscribe(Toggle).AddTo(_disposables);
        }

        public void AttachView(GameObject view)
        {
            view.transform.SetParent(transform, false);
        }

        private void Toggle(bool state)
        {
            gameObject.SetActive(state);

            if (state)
                _viewModel.RequestInstances();
        }
    }
}
