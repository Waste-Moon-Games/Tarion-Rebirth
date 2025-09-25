using Mono.StateMachine;
using Mono.UI.MissionContexUI;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Entry.Mono.MissionPanel
{
    public class StartPrepareMission : MonoBehaviour
    {
        [SerializeField] private GameStateMachineRuntimeSevice _stateMachineMono;
        [SerializeField] private MissionInfoUI _missionInfoUI;

        private Button _startPrepareMissionButton;

        public Button PrepareMissionButton => _startPrepareMissionButton;

        public event Action OnPreparationStarted;

        private void OnEnable()
        {
            if (_stateMachineMono == null)
                _stateMachineMono = FindFirstObjectByType<GameStateMachineRuntimeSevice>();

            if (_startPrepareMissionButton == null)
                _startPrepareMissionButton = GetComponent<Button>();

            if (!_stateMachineMono.SlotsController.HasFreeSlots)
                _startPrepareMissionButton.interactable = false;
            else
                _startPrepareMissionButton.interactable = true;

            _startPrepareMissionButton.onClick.AddListener(PrepareMission);
        }

        private void OnDisable()
        {
            _startPrepareMissionButton.onClick.RemoveListener(PrepareMission);
        }

        private void PrepareMission()
        {
            if (!_stateMachineMono.SlotsController.HasFreeSlots)
                return;

            _stateMachineMono.Run();
            _missionInfoUI.Initialize(_stateMachineMono.LastCreatedContex);

            OnPreparationStarted?.Invoke();
        }
    }
}