using Mono.StateMachine;
using Mono.UI.MissionContexUI;
using UnityEngine;
using UnityEngine.UI;

namespace Entry.Mono.MissionPanel
{
    public class StartPrepareMission : MonoBehaviour
    {
        [SerializeField] private GameStateMachineMono _stateMachineMono;
        [SerializeField] private MissionInfoUI _missionInfoUI;

        private Button _startPrepareMissionButton;

        private void OnEnable()
        {
            if(_startPrepareMissionButton == null)
            {
                _startPrepareMissionButton = GetComponent<Button>();
            }

            _startPrepareMissionButton.onClick.AddListener(PrepareMission);
        }

        private void OnDisable()
        {
            _startPrepareMissionButton.onClick.RemoveListener(PrepareMission);
        }

        private void PrepareMission()
        {
            _stateMachineMono.Run();
            _missionInfoUI.Initialize(_stateMachineMono.StageDependencies.MissionContex);
        }
    }
}