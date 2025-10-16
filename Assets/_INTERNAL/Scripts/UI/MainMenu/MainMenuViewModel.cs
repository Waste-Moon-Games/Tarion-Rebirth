using R3;

namespace UI.MainMenu
{
    public enum MainMenuActions
    {
        GoToMap,
        GoToHeros,
        ToggleMissionPreparation
    }

    public class MainMenuViewModel
    {
        private readonly Subject<Unit> _goToMap;
        private readonly Subject<Unit> _goToHeros;
        private readonly Subject<Unit> _togglePreparationMission;
        private readonly ReactiveProperty<bool> _isMissionPreparationOpen;

        public Observable<MainMenuActions> Actions { get; }
        public ReactiveProperty<bool> IsMissionPreparationOpen => _isMissionPreparationOpen;

        public MainMenuViewModel()
        {
            _goToMap = new Subject<Unit>();
            _goToHeros = new Subject<Unit>();
            _togglePreparationMission = new Subject<Unit>();
            _isMissionPreparationOpen = new(false);

            Actions = Observable
                .Merge(
                _goToMap.Select(_ => MainMenuActions.GoToMap),
                _goToHeros.Select(_ => MainMenuActions.GoToHeros),
                _togglePreparationMission.Select(_ => MainMenuActions.ToggleMissionPreparation));
        }

        public void GoToMap() => _goToMap.OnNext(Unit.Default);
        public void GoToHeros() => _goToHeros.OnNext(Unit.Default);
        public void TogglePreparationWindow() => _togglePreparationMission.OnNext(Unit.Default);
    }
}