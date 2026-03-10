using Core.Common.MVVM;
using R3;
using Scripts.GameEntity.DataInstance;
using System.Collections.Generic;
using UI.HeroMenu.Models;
using UnityEngine;

namespace UI.HeroMenu.ViewModels
{
    public class HeroBarracksViewModel : IViewModel
    {
        private readonly Subject<bool> _modelStateChangedSignal = new();
        private readonly Subject<List<HeroInstance>> _availableHerosRequestSignal = new();
        private readonly Subject<HeroInstance> _heroSelectedSignal = new();

        private readonly CompositeDisposable _disposables = new();

        private HeroBarracksModel _model;

        public Observable<bool> StateChanged => _modelStateChangedSignal.AsObservable();
        public Observable<List<HeroInstance>> AvailableHerosRequest => _availableHerosRequestSignal.AsObservable();
        public Observable<HeroInstance> SelectedHero => _heroSelectedSignal.AsObservable();

        public void BindModel(IModel model)
        {
            _model = model as HeroBarracksModel;

            _model.StateChanged.Subscribe(HandleStateChanged).AddTo(_disposables);
            _model.AvailableHerosRequest.Subscribe(HandleAvailableHeros).AddTo(_disposables);
            _model.SelectedHero.Subscribe(HandleSelectedHero).AddTo(_disposables);
        }

        public void RequestInstances()
        {
            _model.RequestAvailableHeros();
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }

        private void HandleSelectedHero(HeroInstance selectedHero) => _heroSelectedSignal.OnNext(selectedHero);
        private void HandleAvailableHeros(List<HeroInstance> aHeros) => _availableHerosRequestSignal.OnNext(aHeros);
        private void HandleStateChanged(bool state) => _modelStateChangedSignal.OnNext(state);
    }
}