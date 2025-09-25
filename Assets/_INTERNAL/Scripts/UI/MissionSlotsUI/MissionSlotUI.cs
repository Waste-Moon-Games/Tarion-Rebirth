using Core.MissionSlots;
using Mono.StateMachine;
using Mono.UI.MissionContexUI;
using UI.GameUIBridges;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MissionSlotsUI
{
    public class MissionSlotUI : MonoBehaviour
    {
        [Header("Brigde")]
        [SerializeField] private MissionUIBridge _bridge;

        [Space(10), Header("Mission info UI")]
        [SerializeField] private MissionInfoUI _missionUI;

        [Space(10), Header("Slot ID")]
        [SerializeField] private int _slotID;

        private Button _openMissionUIButton;

        [SerializeField] private GameStateMachineRuntimeSevice _stateMachineMono;
        private MissionSlot _slot;

        private void OnDestroy()
        {
            _openMissionUIButton.onClick.RemoveListener(OpenMissionInfoUI);
        }

        private void Start()
        {
            if (_stateMachineMono == null)
                _stateMachineMono = FindFirstObjectByType<GameStateMachineRuntimeSevice>();

            if (_stateMachineMono.SlotsController.Slots != null)
                _slot = _stateMachineMono.SlotsController.Slots[_slotID];

            if (_openMissionUIButton == null)
                _openMissionUIButton = GetComponent<Button>();

            _openMissionUIButton.onClick.AddListener(OpenMissionInfoUI);

            if (_slot.IsRunning)
                _missionUI.Initialize(_slot.Contex);
        }

        private void OpenMissionInfoUI()
        {
            _missionUI.Show();

            if (_slot.IsRunning)
                _missionUI.Initialize(_slot.Contex);
        }
    }
}