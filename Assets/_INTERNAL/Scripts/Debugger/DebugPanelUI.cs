using Core.Contex.Debug;
using Mono.StateMachine;
using UnityEngine;
using UnityEngine.UI;

namespace Debugger
{
    public class DebugPanelUI : MonoBehaviour
    {
        [SerializeField] private GameStateMachineMono _stateMachineMono;

        [SerializeField] private GameObject _panelRoot;
        [SerializeField] private Button _addExpButton;
        [SerializeField] private Button _completeMissionButton;

        private void Awake()
        {
#if UNITY_EDITOR || DEBUG
            _panelRoot.SetActive(false);
            _addExpButton.onClick.AddListener(()=> DebugActions.AddExpToCurrentHero(100));
            _completeMissionButton.onClick.AddListener(()=> DebugActions.ForceCompleteMission());
            _stateMachineMono.OnGameStageControllerInitialized += HandleInitializedStageController;
#else
            Destroy(gameObject);
#endif
        }

        private void HandleInitializedStageController(Stages.StageController.GameStageController obj)
        {
            DebugContex.SetController(obj);
        }

        public void Toggle()
        {
            _panelRoot.SetActive(!_panelRoot.activeSelf);
        }
    }
}