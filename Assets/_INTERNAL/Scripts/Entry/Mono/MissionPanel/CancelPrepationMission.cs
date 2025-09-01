using Mono.StateMachine;
using UnityEngine;
using UnityEngine.UI;

namespace Entry.Mono.MissionPanel
{
    public class CancelPrepationMission : MonoBehaviour
    {
        [SerializeField] private GameStateMachineMono _gameStateMachine;
        [SerializeField] private StartPrepareMission _startMissionButton;

        private Button _cancelButton;

        private void OnEnable()
        {
            if(_cancelButton == null)
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