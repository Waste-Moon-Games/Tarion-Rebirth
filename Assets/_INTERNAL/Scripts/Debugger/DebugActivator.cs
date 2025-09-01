using UnityEngine;
using UnityEngine.InputSystem;

namespace Debugger
{
    public class DebugActivator : MonoBehaviour
    {
        [SerializeField] private DebugPanelUI _debugPanelUI;

        private InputAction _toggleAction;

        private void Awake()
        {
#if UNITY_EDITOR || DEBUG
            _toggleAction = new(type: InputActionType.Button, binding: "<Keyboard>/f12");
            _toggleAction.Enable();
#endif
        }

        private void OnDestroy()
        {
#if UNITY_EDITOR || DEBUG
            _toggleAction?.Dispose();
#endif
        }

        private void Update()
        {
#if UNITY_EDITOR || DEBUG
            if (_toggleAction.WasPerformedThisFrame())
            {
                _debugPanelUI.Toggle();
            }
#endif
        }
    }
}