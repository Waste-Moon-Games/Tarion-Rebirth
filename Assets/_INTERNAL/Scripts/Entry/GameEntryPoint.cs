using Core.Consts;
using Core.DI;
using Core.GameStates;
using Entry.Mono.ScenesEntry;
using Mono.InstanceInitialize;
using R3;
using UI.Base;
using UI.MainMenu;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils.ModCoroutines;
using Utils.SceneLoader;

namespace Entry
{
    public class GameEntryPoint
    {
        private static GameEntryPoint _instance;

        private readonly DIContainer _rootContainer = new();
        private readonly SceneNavigatorService _sceneNavigatorService;

        private readonly UIRootView _uiRoot;
        private readonly Coroutines _coroutines;

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static void AutoStartGame()
        {
            Application.targetFrameRate = 60;

            Screen.autorotateToLandscapeRight = true;
            Screen.autorotateToLandscapeLeft = true;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;

            _instance = new GameEntryPoint();
            _instance.RunGame();
        }

        private GameEntryPoint()
        {
            UIRootView prefabUIRoot = Resources.Load<UIRootView>("UILoadingScreen");
            Coroutines coroutinesPrefab = Resources.Load<Coroutines>("Utils/[COROUTINES]");
            BootUniqueDatas uniqueDatas = Resources.Load<BootUniqueDatas>("Boot/BootUniqueData");

            _uiRoot = Object.Instantiate(prefabUIRoot);
            _coroutines = Object.Instantiate(coroutinesPrefab);

            RegisterGlobalServices(uniqueDatas);
            _sceneNavigatorService = new(_rootContainer.Resolve<SceneLoaderService>(), _rootContainer);
        }

        private void RegisterGlobalServices(BootUniqueDatas uniqueDatas)
        {
            _rootContainer.RegisterInstance(_uiRoot);
            _rootContainer.RegisterInstance(_coroutines);

            _rootContainer.RegisterFactory(gs => new GameState(
                uniqueDatas.HeroDatas,
                uniqueDatas.PlanetDatas,
                uniqueDatas.MissionDatas,
                uniqueDatas.RankProgressionConfig,
                uniqueDatas.ImperiumConfig,
                _rootContainer.Resolve<Coroutines>()))
                .AsSingle();
            _rootContainer.RegisterFactory(
                c => new SceneLoaderService(c.Resolve<UIRootView>(),
                c.Resolve<Coroutines>()))
                .AsSingle();
        }

        private void RunGame()
        {
            _sceneNavigatorService.StartFromMainMenu();
        }
    }
}