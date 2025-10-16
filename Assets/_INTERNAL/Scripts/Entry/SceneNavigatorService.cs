using Core.Consts;
using Core.DI;
using Entry.Mono.ScenesEntry;
using Entry.Mono.ScenesEntry.MainMenu;
using R3;
using System;
using UI.MainMenu;
using Utils.SceneLoader;
using Object = UnityEngine.Object;

namespace Entry
{
    public class SceneNavigatorService : IDisposable
    {
        private readonly SceneLoaderService _sceneLoader;
        private readonly DIContainer _rootContainer;
        private readonly CompositeDisposable _disposables = new();

        private DIContainer _cachedContainer;

        public SceneNavigatorService(SceneLoaderService sceneLoader, DIContainer rootContainer)
        {
            _sceneLoader = sceneLoader;
            _rootContainer = rootContainer;
        }

        public void StartFromMainMenu()
        {
            LoadScene(SceneNames.MAIN_MENU);
        }

        private void LoadScene(string sceneName)
        {
            _cachedContainer = null;

            _sceneLoader.OnSceneLoaded
                .Where(loadedScene => loadedScene == sceneName)
                .Take(1)
                .Subscribe(_ => OnSceneLoaded(sceneName))
                .AddTo(_disposables);

            _sceneLoader.LoadScene(sceneName);
        }

        private void OnSceneLoaded(string sceneName)
        {
            switch (sceneName)
            {
                case SceneNames.MAIN_MENU:
                    CreateMainMenu();
                    break;
                case SceneNames.GALAXY_MAP:
                    CreateGalaxyMapMenu();
                    break;
                case SceneNames.HEROS_MENU:
                    CreateHerosMenu();
                    break;
            }
        }

        private void CreateMainMenu()
        {
            var container = _cachedContainer = new(_rootContainer);
            var entryPoint = Object.FindFirstObjectByType<MainMenuEntryPoint>();

            entryPoint.Run(container)
                .Take(1)
                .Subscribe(action =>
            {
                switch (action)
                {
                    case MainMenuActions.GoToMap:
                        LoadScene(SceneNames.GALAXY_MAP);
                        break;
                    case MainMenuActions.GoToHeros:
                        LoadScene(SceneNames.HEROS_MENU);
                        break;
                }
            }).AddTo(_disposables);
        }

        private void CreateGalaxyMapMenu()
        {
            var container = _cachedContainer = new(_rootContainer);
            var entryPoint = Object.FindFirstObjectByType<MapMenuEntryPoint>();

            entryPoint.Run(container)
                .Take(1)
                .Subscribe(_ => LoadScene(SceneNames.MAIN_MENU))
                .AddTo(_disposables);
        }

        private void CreateHerosMenu()
        {
            var container = _cachedContainer = new(_rootContainer);
            var entryPoint = Object.FindFirstObjectByType<HerosMenuEntryPoint>();

            entryPoint.Run(container)
                .Take(1)
                .Subscribe(_ => LoadScene(SceneNames.MAIN_MENU))
                .AddTo(_disposables);
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}