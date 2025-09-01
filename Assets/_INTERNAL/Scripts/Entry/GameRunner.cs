using SO.Containers;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Entry
{
    public class GameRunner : MonoBehaviour
    {
        [SerializeField] private Image _progressBar;
        [SerializeField] private GameScene _loadableScene;

        private AsyncOperation _loadingOperation;

        private void Awake()
        {
            _progressBar.fillAmount = 0f;
        }

        private IEnumerator Start()
        {
            yield return new WaitForEndOfFrame();

            _loadingOperation = SceneManager.LoadSceneAsync(_loadableScene.SceneName);
            _loadingOperation.allowSceneActivation = false;

            while (!_loadingOperation.isDone)
            {
                CheckLoadingState();
                yield return null;
            }
        }

        private void CheckLoadingState()
        {
            float progress = Mathf.Clamp01(_loadingOperation.progress / 0.9f);

            _progressBar.fillAmount = progress;

            Debug.Log($"Loading progress: {progress * 100}%");

            if (_loadingOperation.progress >= 0.9f)
            {
                _loadingOperation.allowSceneActivation = true;
            }
        }
    }
}