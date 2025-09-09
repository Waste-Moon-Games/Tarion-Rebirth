using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Debugger
{
    public class DebugActivator : MonoBehaviour
    {
        [SerializeField] private DebugPanelUI _debugPanelUI;
        [SerializeField] private Button _toggleButton;

        private InputAction _toggleAction;

        private void OnEnable()
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD

            _toggleButton.onClick.AddListener(ToggleDebugger);
#endif
        }

        private void OnDisable()
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            _toggleButton.onClick.RemoveListener(ToggleDebugger);
#endif
        }

        private void Awake()
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            _toggleAction = new(type: InputActionType.Button, binding: "<Keyboard>/f12");
            _toggleAction.Enable();
#endif
        }

        private void OnDestroy()
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            _toggleAction?.Dispose();
#endif
        }

        private void Update()
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            if (_toggleAction.WasPerformedThisFrame())
            {
                _debugPanelUI.Toggle();
            }
#endif
        }

        private void ToggleDebugger()
        {
#if UNITY_EDITOR || DEVELOPMENT_BUILD
            _debugPanelUI.Toggle();
#endif
        }
    }
}