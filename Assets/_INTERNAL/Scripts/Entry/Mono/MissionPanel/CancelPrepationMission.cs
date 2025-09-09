using Mono.StateMachine;
using UnityEngine;
using UnityEngine.UI;

namespace Entry.Mono.MissionPanel
{
    public class CancelPrepationMission : MonoBehaviour
    {
        [SerializeField] private StartPrepareMission _startMissionButton;

        private GameStateMachineRuntimeSevice _gameStateMachine;
        private Button _cancelButton;

        private void OnEnable()
        {
            if(_gameStateMachine == null)
                _gameStateMachine = FindFirstObjectByType<GameStateMachineRuntimeSevice>();

            if (_cancelButton == null)
                _cancelButton = GetComponent<Button>();

            _cancelButton.onClick.AddListener(CancelPreparateMission);

            _startMissionButton.OnPreparationStarted += HandleStartedPreparation;
            _gameStateMachine.OnMissionStarted += HandleStartedMission;
        }

        private void OnDisable()
        {
            _cancelButton.onClick.RemoveListener(CancelPreparateMission);

            _gameStateMachine.OnMissionStarted -= HandleStartedMission;
        }

        private void OnDestroy()
        {
            if(!_gameStateMachine.IsRunning)
                _gameStateMachine.ForceEnd();
        }

        private void CancelPreparateMission()
        {
            _gameStateMachine.ForceEnd();
            _startMissionButton.PrepareMissionButton.interactable = true;
            _cancelButton.interactable = false;
        }

        private void HandleStartedMission()
        {
            _cancelButton.interactable = false;
        }

        private void HandleStartedPreparation()
        {
            _cancelButton.interactable = true;
        }
    }
}