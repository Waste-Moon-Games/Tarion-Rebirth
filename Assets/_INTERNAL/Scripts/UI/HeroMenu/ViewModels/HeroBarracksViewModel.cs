using Core.Common.MVVM;
using GameEntity.DataInstance.Main;
using R3;
using Scripts.GameEntity.DataInstance;
using System.Collections.Generic;
using UI.HeroMenu.Models;

namespace UI.HeroMenu.ViewModels
{
    public class HeroBarracksViewModel : IViewModel
    {
        private readonly Subject<bool> _modelStateChangedSignal = new();
        private readonly Subject<ImperiumInstancesHolder> _availableHerosRequestSignal = new();

        private readonly CompositeDisposable _disposables = new();

        private HeroBarracksModel _model;

        public Observable<bool> StateChanged => _modelStateChangedSignal.AsObservable();
        public Observable<ImperiumInstancesHolder> AvailableHerosRequest => _availableHerosRequestSignal.AsObservable();

        public void BindModel(IModel model)
        {
            _model = model as HeroBarracksModel;

            _model.StateChanged.Subscribe(state => _modelStateChangedSignal.OnNext(state)).AddTo(_disposables);
            _model.AvailableHerosRequest.Subscribe(holder => _availableHerosRequestSignal.OnNext(holder)).AddTo(_disposables);
        }

        public void RequestInstances()
        {
            _model.RequestAvailableHeros();
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}