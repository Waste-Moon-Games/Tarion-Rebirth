using Core.Common;
using Cysharp.Threading.Tasks;
using R3;
using System.Collections;
using System.Threading.Tasks;
using UI.Base;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils.ModCoroutines;

namespace Utils.SceneLoader
{
    public class SceneLoaderService : ISceneLoaderService
    {
        private readonly UILoadingView _loadindScreen;

        private readonly Subject<float> _progressUpdated;
        private readonly Subject<string> _sceneLoaded;

        public Subject<float> OnProgressUpdated => _progressUpdated;

        public Subject<string> OnSceneLoaded => _sceneLoaded;

        public SceneLoaderService(UILoadingView loadindScreen)
        {
            _loadindScreen = loadindScreen;
            _progressUpdated = new Subject<float>();
            _sceneLoaded = new Subject<string>();
        }

        public void LoadScene(string sceneName)
        {
            LoadSceneRoutine(sceneName).Forget();
        }

        private async UniTask LoadSceneRoutine(string sceneName)
        {
            _loadindScreen.ShowLoadingScreen();

            AsyncOperation asyncOp = SceneManager.LoadSceneAsync(sceneName);
            asyncOp.allowSceneActivation = true;

            while (!asyncOp.isDone)
            {
                _loadindScreen.SetLoadingProgress(asyncOp.progress / 0.9f);
                OnProgressUpdated?.OnNext(asyncOp.progress / 0.9f);
                await UniTask.Yield();
            }

            _loadindScreen.HideLoadingScreen();

            OnSceneLoaded?.OnNext(sceneName);
        }
    }
}