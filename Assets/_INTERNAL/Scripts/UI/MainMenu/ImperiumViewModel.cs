using Core.EntityDatas.ImperiumInfo;
using Core.EntityDatas.Resource;
using Core.GameStates;
using Core.GameStates.Extensions;
using Core.Instances.Extensions;
using R3;

namespace UI.MainMenu
{
    public class ImperiumViewModel
    {
        private readonly ImperiumState _imperiumState;

        private int _currentHeroCount;
        private int _maxHeroCount;

        private int _currentPlanetCount;
        private int _maxPlanetCount;

        private int _voidMatterCount;
        private int _darkEnergyCount;
        private int _mineralsCount;

        private readonly CompositeDisposable _disposables = new();

        private readonly Subject<(int, InstanceUpdateType)> _instanceUpdates;

        private readonly Subject<(int, ResourceType)> _resourceUpdates;

        public int CurrentHeroCount => _currentHeroCount;
        public int MaxHerosCount => _maxHeroCount;

        public int CurrentPlanetCount => _currentPlanetCount;
        public int MaxPlanetsCount => _maxPlanetCount;

        public int VoidMatterCount => _voidMatterCount;
        public int DarkEnergyCount => _darkEnergyCount;
        public int MineralsCount => _mineralsCount;

        public Observable<(int, InstanceUpdateType)> ImperiumInstancesInfo { get; }
        public Observable<(int, ResourceType)> ImperiumResourcesInfo { get; }

        public ImperiumViewModel(ImperiumState imperium)
        {
            _imperiumState = imperium;

            _currentHeroCount = _imperiumState.InstanceHolder.Heros.Count;
            _maxHeroCount = _imperiumState.InstanceHolder.MaxHeros;

            _currentPlanetCount = _imperiumState.InstanceHolder.Planets.Count;
            _maxPlanetCount = _imperiumState.InstanceHolder.MaxPlanets;

            _voidMatterCount = _imperiumState.ImperiumResource.Get(ResourceType.Void_Matter);
            _darkEnergyCount = _imperiumState.ImperiumResource.Get(ResourceType.Dark_Energy);
            _mineralsCount = _imperiumState.ImperiumResource.Get(ResourceType.Mineral_Crystalls);

            _instanceUpdates = new();
            _resourceUpdates = new();

            ImperiumInstancesInfo = _instanceUpdates.AsObservable();

            ImperiumResourcesInfo = _resourceUpdates.AsObservable();
        }

        public void Subscribe()
        {
            _imperiumState.ImperiumResource.OnResourceChangedAsObservable()
                .Subscribe(v => HandleResourcesCountUpdated(v.type, v.value))
                .AddTo(_disposables);

            _imperiumState.InstanceHolder.OnHerosCountChangedAsObservable()
                .Subscribe(HandleCurrentHerosCountUpdated)
                .AddTo(_disposables);

            _imperiumState.InstanceHolder.OnPlanetsCountChangedAsObservable()
                .Subscribe(HandleCurrentPlanetsCountUpdated)
                .AddTo(_disposables);

            _imperiumState.InstanceHolder.OnHerosLimitChangedAsObservable()
                .Subscribe(HandleMaxHerosCountUpdated)
                .AddTo(_disposables);

            _imperiumState.InstanceHolder.OnPlanetsLimitChangedAsObservable()
                .Subscribe(HandleMaxPlanetsCountUpdated)
                .AddTo(_disposables);
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }

        private void HandleCurrentHerosCountUpdated(int value)
        {
            _currentHeroCount = value;
            _instanceUpdates.OnNext((value, InstanceUpdateType.HeroCount));
        }

        private void HandleMaxHerosCountUpdated(int value)
        {
            _maxHeroCount = value;
            _instanceUpdates.OnNext((value, InstanceUpdateType.MaxHeroCount));
        }

        private void HandleCurrentPlanetsCountUpdated(int value)
        {
            _currentPlanetCount = value;
            _instanceUpdates.OnNext((value, InstanceUpdateType.PlanetCount));

        }

        private void HandleMaxPlanetsCountUpdated(int value)
        {
            _maxPlanetCount = value;
            _instanceUpdates.OnNext((value, InstanceUpdateType.MaxPlanetCount));
        }

        private void HandleResourcesCountUpdated(ResourceType type, int value)
        {
            switch (type)
            {
                case ResourceType.Void_Matter:
                    _voidMatterCount = value;
                    break;
                case ResourceType.Dark_Energy:
                    _darkEnergyCount = value;
                    break;
                case ResourceType.Mineral_Crystalls:
                    _mineralsCount = value;
                    break;
            }

            _resourceUpdates.OnNext((value, type));
        }
    }
}