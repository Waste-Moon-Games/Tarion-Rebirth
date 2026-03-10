using Core.Common.MVVM;
using R3;
using Scripts.GameEntity.DataInstance;
using UI.HeroMenu.Models;

namespace UI.HeroMenu.ViewModels
{
    public class RecruitHerosViewModel : IViewModel
    {
        private readonly CompositeDisposable _disposables = new();

        private readonly Subject<bool> _modelStateChangedSignal = new();
        private readonly Subject<Unit> _refreshSignal = new();
        private readonly Subject<HeroInstance> _selectedHeroSignal = new();

        private RecruitHerosModel _model;

        public Observable<bool> StateChanged => _modelStateChangedSignal.AsObservable();
        public Observable<HeroInstance> SelectedHero => _selectedHeroSignal.AsObservable();
        public Observable<Unit> Refreshed => _refreshSignal.AsObservable();

        public void BindModel(IModel model)
        {
            _model = model as RecruitHerosModel;
            _model.StateChanged.Subscribe(HandleChangedState).AddTo(_disposables);
            _model.HeroSelected.Subscribe(HandleSelectedHero).AddTo(_disposables);
            _model.Refreshed.Subscribe(_ => HandleRefreshedSignal()).AddTo(_disposables);
        }

        public void Dispose() => _disposables.Dispose();

        private void HandleChangedState(bool state) => _modelStateChangedSignal.OnNext(state);
        private void HandleSelectedHero(HeroInstance selectedHero) => _selectedHeroSignal.OnNext(selectedHero);
        private void HandleRefreshedSignal() => _refreshSignal.OnNext(Unit.Default);
    }
}