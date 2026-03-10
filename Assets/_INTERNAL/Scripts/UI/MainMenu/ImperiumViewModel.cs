using Core.Common.MVVM;
using Core.EntityDatas.ImperiumInfo;
using Core.EntityDatas.Resource;
using Core.GameStates;
using R3;
using System.Collections.Generic;

namespace UI.MainMenu
{
    public class ImperiumViewModel : IViewModel
    {
        private ImperiumState _model;

        private int _currentHeroCount;
        private int _maxHeroCount;

        private int _currentPlanetCount;
        private int _maxPlanetCount;

        private int _voidMatterCount;
        private int _darkEnergyCount;
        private int _mineralsCount;

        private readonly CompositeDisposable _disposables = new();

        private readonly Subject<(int, InstanceUpdateType)> _instanceUpdatesSignal = new();
        private readonly Subject<(int, ResourceType)> _resourceUpdatesSignal = new();

        public int CurrentHeroCount => _currentHeroCount;
        public int MaxHerosCount => _maxHeroCount;

        public int CurrentPlanetCount => _currentPlanetCount;
        public int MaxPlanetsCount => _maxPlanetCount;

        public int VoidMatterCount => _voidMatterCount;
        public int DarkEnergyCount => _darkEnergyCount;
        public int MineralsCount => _mineralsCount;

        public Observable<(int, InstanceUpdateType)> ImperiumInstancesInfo => _instanceUpdatesSignal.AsObservable();
        public Observable<(int, ResourceType)> ImperiumResourcesInfo => _resourceUpdatesSignal.AsObservable();

        public void BindModel(IModel model)
        {
            _model = model as ImperiumState;

            _model.ResourcesStateRequest.Subscribe(HandleResourcesCountUpdated).AddTo(_disposables);

            _model.CurrentCountRequest.Subscribe(value =>
            {
                HandleCurrentHerosCountUpdated(value.Item1);
                HandleCurrentPlanetsCountUpdated(value.Item2);
            }).AddTo(_disposables);
            _model.MaxCountRequest.Subscribe(value =>
            {
                HandleMaxHerosCountUpdated(value.Item1);
                HandleMaxPlanetsCountUpdated(value.Item2);
            }).AddTo(_disposables);

            _model.RequestCurrentInstancesCount();
            _model.RequestMaxInstancesCount();
            _model.RequestResourcesState();
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }

        private void HandleCurrentHerosCountUpdated(int value)
        {
            _currentHeroCount = value;
            _instanceUpdatesSignal.OnNext((value, InstanceUpdateType.HeroCount));
        }

        private void HandleMaxHerosCountUpdated(int value)
        {
            _maxHeroCount = value;
            _instanceUpdatesSignal.OnNext((value, InstanceUpdateType.MaxHeroCount));
        }

        private void HandleCurrentPlanetsCountUpdated(int value)
        {
            _currentPlanetCount = value;
            _instanceUpdatesSignal.OnNext((value, InstanceUpdateType.PlanetCount));

        }

        private void HandleMaxPlanetsCountUpdated(int value)
        {
            _maxPlanetCount = value;
            _instanceUpdatesSignal.OnNext((value, InstanceUpdateType.MaxPlanetCount));
        }

        private void HandleResourcesCountUpdated(Dictionary<ResourceType, int> resources)
        {
            foreach (KeyValuePair<ResourceType, int> resource in resources)
            {
                switch (resource.Key)
                {
                    case ResourceType.Void_Matter:
                        _voidMatterCount = resource.Value;
                        _resourceUpdatesSignal.OnNext((resource.Value, resource.Key));
                        break;
                    case ResourceType.Mineral_Crystalls:
                        _mineralsCount = resource.Value;
                        _resourceUpdatesSignal.OnNext((resource.Value, resource.Key));
                        break;
                    case ResourceType.Dark_Energy:
                        _darkEnergyCount = resource.Value;
                        _resourceUpdatesSignal.OnNext((resource.Value, resource.Key));
                        break;
                }
            }
        }
    }
}