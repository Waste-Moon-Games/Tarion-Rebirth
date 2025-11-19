using UnityEngine;

namespace UI.Base
{
    public class UIRootView : MonoBehaviour
    {
        [SerializeField] private Transform _uiSceneContainer;

        public void AttachSceneUI(GameObject sceneUI)
        {
            sceneUI.transform.SetParent(_uiSceneContainer, false);
        }
    }
}