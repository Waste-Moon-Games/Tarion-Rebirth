using Core.Common.MVVM;
using R3;
using UI.HeroMenu.Models;

namespace UI.HeroMenu.ViewModels
{
    public class RecruitHerosViewModel : IViewModel
    {
        private readonly CompositeDisposable _disposables = new();
        private readonly Subject<bool> _modelStateChangedSignal = new();

        private RecruitHerosModel _model;

        public Observable<bool> StateChanged => _modelStateChangedSignal.AsObservable();

        public void BindModel(IModel model)
        {
            _model = model as RecruitHerosModel;
            _model.StateChanged.Subscribe(state => _modelStateChangedSignal.OnNext(state)).AddTo(_disposables);
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}