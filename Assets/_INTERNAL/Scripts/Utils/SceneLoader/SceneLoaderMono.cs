using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utils.SceneLoader
{
    public class SceneLoaderMono : MonoBehaviour
    {
        public static SceneLoaderMono Instance;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
        }

        public void OpenScene(string sceneToUnload, string sceneToLoad)
        {
            StartCoroutine(LoadAndUnloadScenes(sceneToUnload, sceneToLoad));
        }

        private IEnumerator LoadAndUnloadScenes(string sceneToUnload, string sceneToLoad)
        {
            AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);

            while (!loadOperation.isDone)
            {
                yield return null;
            }

            Scene newScene = SceneManager.GetSceneByName(sceneToLoad);
            if (newScene.IsValid())
            {
                SceneManager.SetActiveScene(newScene);
            }

            AsyncOperation unloadOperation = SceneManager.UnloadSceneAsync(sceneToUnload);
            if (unloadOperation != null)
            {
                while (!unloadOperation.isDone)
                {
                    yield return null;
                }
            }
        }
    }
}