using SO.Containers;
using UI.Base;
using UnityEngine;
using Utils.SceneLoader;

namespace HeroDetailInfoUI
{
    public class ToggleHeroInfoHolder : SimpleWindowToggler
    {
        [Header("Scene to load")]
        [SerializeField] private GameScene _sceneToLoad;

        [Space(10), Header("Scene to unload")]
        [SerializeField] private GameScene _sceneToUnload;

        private void OnEnable()
        {
            _toggleButton.onClick.RemoveAllListeners();
            _toggleButton.onClick.AddListener(OpenMapScene);
        }

        private void OnDisable()
        {
            _toggleButton.onClick.RemoveListener(OpenMapScene);
        }

        private void OpenMapScene()
        {
            SceneLoaderMono.Instance.OpenScene(_sceneToUnload.SceneName, _sceneToLoad.SceneName);
        }
    }
}