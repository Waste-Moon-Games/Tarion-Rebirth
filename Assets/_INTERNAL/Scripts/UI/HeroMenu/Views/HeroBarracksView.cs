using Core.Common.MVVM;
using GameEntity.DataInstance.Main;
using R3;
using UI.HeroDetailInfoUI;
using UI.HeroMenu.ViewModels;
using UnityEngine;

namespace UI.HeroMenu.Views
{
    public class HeroBarracksView : MonoBehaviour, IView
    {
        [field: SerializeField] public OwnedHeroInfoHolderView OwnedHeroInfoHolderView { get; private set; }

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
            _viewModel.AvailableHerosRequest.Subscribe(InitAvailableHeros).AddTo(_disposables);
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

        private void InitAvailableHeros(ImperiumInstancesHolder instancesHolder)
        {
            //OwnedHeroInfoHolderView.Init(instancesHolder);
        }
    }
}
