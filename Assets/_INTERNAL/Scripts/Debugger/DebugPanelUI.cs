using Core.Contex.Debug;
using Entry.Mono;
using Mono.StateMachine;
using UnityEngine;
using UnityEngine.UI;

namespace Debugger
{
    public class DebugPanelUI : MonoBehaviour
    {
        [SerializeField] private GameStateMachineRuntimeSevice _stateMachineMono;

        [SerializeField] private GameObject _panelRoot;
        [SerializeField] private Button _addExpButton;
        [SerializeField] private Button _completeMissionButton;

        private void Awake()
        {
#if UNITY_EDITOR || DEBUG
            _stateMachineMono = FindFirstObjectByType<GameStateMachineRuntimeSevice>();
            _panelRoot.SetActive(false);
            _addExpButton.onClick.AddListener(()=> DebugActions.AddExpToCurrentHero(100));
            _completeMissionButton.onClick.AddListener(()=> DebugActions.ForceCompleteMission());
            //_stateMachineMono.OnGameStageControllerInitialized += HandleInitializedStageController;
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