using Core.Common.MVVM;
using GameEntity.DataInstance.Main;
using R3;
using Scripts.GameEntity.DataInstance;
using System.Collections.Generic;

namespace UI.HeroMenu.Models
{
    public class HeroBarracksModel : IModel
    {
        private readonly Subject<bool> _stateChangedSignal = new();
        private readonly Subject<ImperiumInstancesHolder> _availableHerosRequestSignal = new();
        private readonly Subject<HeroInstance> _heroListUpdatedSignal = new();
        private readonly Subject<HeroInstance> _heroSelectedSignal = new();

        private readonly ImperiumInstancesHolder _instancesHolder;

        private bool _state = true;

        public Observable<bool> StateChanged => _stateChangedSignal.AsObservable();
        public Observable<ImperiumInstancesHolder> AvailableHerosRequest => _availableHerosRequestSignal.AsObservable();
        public Observable<HeroInstance> HeroListUpdated => _heroListUpdatedSignal.AsObservable();
        public Observable<HeroInstance> SelectedHero => _heroSelectedSignal.AsObservable();

        public HeroBarracksModel(ImperiumInstancesHolder instancesHolder)
        {
            _instancesHolder = instancesHolder;
            _instancesHolder.HerosListUpdated.Subscribe(HandleHerosListUpdated);
        }

        /// <summary>
        /// Открыть Казармы
        /// </summary>
        public void Open()
        {
            _state = true;
            _stateChangedSignal.OnNext(_state);
        }

        /// <summary>
        /// Закрыть Казармы
        /// </summary>
        public void Close()
        {
            _state = false;
            _stateChangedSignal.OnNext(_state);
        }

        /// <summary>
        /// Запросить доступных героев
        /// </summary>
        public void RequestAvailableHeros()
        {
            _availableHerosRequestSignal.OnNext(_instancesHolder);
        }

        public void SelectHero(HeroInstance selectedHero) => _heroSelectedSignal.OnNext(selectedHero);

        private void HandleHerosListUpdated(HeroInstance newHero) => _heroListUpdatedSignal.OnNext(newHero);
    }
}