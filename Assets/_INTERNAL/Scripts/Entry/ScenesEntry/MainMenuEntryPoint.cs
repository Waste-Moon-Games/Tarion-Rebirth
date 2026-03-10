using Core.DI;
using Core.GameStates;
using Mono.StateMachine;
using R3;
using UI.Binders.MainMenu;
using UI.MainMenu;
using UnityEngine;

namespace Entry.Mono.ScenesEntry.MainMenu
{
    public class MainMenuEntryPoint : MonoBehaviour
    {
        [Header("Prefabs")]
        [SerializeField] private UIMainMenuRootBinder _binderPrefab;
        [SerializeField] private UIMainMenuView _viewPrefab;
        [SerializeField] private ImperiumHUDView _hudViewPrefab;

        [Space(10), Header("UI Deps")]
        [SerializeField] private GameObject _missionPreparationWindow;
        [SerializeField] private StateMachineUIDependencies _dependencies;
        [SerializeField] private MonoStageMachineDependencies _machineDependencies;

        private MainMenuViewModel _mainMenuViewModel;
        private UIMainMenuRootBinder _binder;
        private UIMainMenuView _view;

        private ImperiumViewModel _imperiumViewModel;
        private ImperiumHUDView _hudView;

        private void OnDestroy()
        {
            _imperiumViewModel.Dispose();
        }

        public Observable<MainMenuActions> Run(DIContainer mainMenuContainer)
        {
            DIContainer mainMenuViewModelsContainer = CreateMainMenu(mainMenuContainer);

            var gameStateMachineRuntimeService = FindFirstObjectByType<GameStateMachineRuntimeSevice>();
            var missionRuntimeService = mainMenuContainer.Resolve<GameState>().MissionRuntimeService;
            var imperiumState = mainMenuContainer.Resolve<GameState>().ImperiumState;

            gameStateMachineRuntimeService.Init(missionRuntimeService, imperiumState);

            mainMenuViewModelsContainer.RegisterInstance(_mainMenuViewModel);

            _binder.gameObject.transform.SetParent(_view.transform, false);
            _view.gameObject.transform.SetParent(_view.transform, false);
            _view.SetMissionWindow(_missionPreparationWindow);

            _missionPreparationWindow = null;

            _binder.Bind(_view, _mainMenuViewModel);
            _hudView.BindViewModel(_imperiumViewModel);
            _dependencies.Init();

            for (int i = 0; i < _view.Timers.Count; i++)
            {
                _dependencies.SetMissionExecutionUI(_view.Timers[i]);
            }

            _machineDependencies.Init();

            return _mainMenuViewModel.Actions.Where(action => action != MainMenuActions.ToggleMissionPreparation);
        }

        private DIContainer CreateMainMenu(DIContainer mainMenuContainer)
        {
            var imperiumState = mainMenuContainer.Resolve<GameState>().ImperiumState;

            _binder = _binder == null ? Instantiate(_binderPrefab) : _binder;
            _view = _view == null ? Instantiate(_viewPrefab) : _view;
            _hudView = _hudView == null ? Instantiate(_hudViewPrefab) : _hudView;

            _mainMenuViewModel ??= new();
            _imperiumViewModel ??= new();
            _imperiumViewModel.BindModel(imperiumState);

            return mainMenuContainer;
        }
    }
}