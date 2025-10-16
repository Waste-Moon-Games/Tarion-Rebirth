using System.Collections.Generic;
using UI.MainMenu.MissionExecutionUI;
using UI.MissionContexUI;
using UI.Result;
using UI.SelectionPanel;
using UnityEngine;

namespace Mono.StateMachine
{
    public class StateMachineUIDependencies : MonoBehaviour
    {
        [field: SerializeField] public SelectionPanel SelectionPanel {  get; private set; }
        [field: SerializeField] public MissionPreparationUI MissionPreparationUI { get; private set; }
        [field: SerializeField] public List<MissionExecutionTimer> MissionExecutionUI { get; private set; }
        [field: SerializeField] public ResultPanel ResultPanelHolder { get; private set; }

        public void Init()
        {
            MissionExecutionUI = new();
        }

        public void SetMissionExecutionUI(MissionExecutionTimer timer)
        {
            if (MissionExecutionUI.Contains(timer))
                return;

            MissionExecutionUI.Add(timer);
        }

        private void OnDestroy()
        {
            MissionExecutionUI.Clear();
        }
    }
}