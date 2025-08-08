using UnityEngine;

namespace Debugger
{
    public class DebugActivator : MonoBehaviour
    {
        [SerializeField] private DebugPanelUI _debugPanelUI;

        private void Update()
        {
#if UNITY_EDITOR || DEBUG
            if (Input.GetKeyDown(KeyCode.F12))
            {
                _debugPanelUI.Toggle();
            }
#endif
        }
    }
}