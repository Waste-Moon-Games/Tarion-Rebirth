using UI.MissionContexUI;
using UI.MissionExecutionUI;
using UI.Result;
using UI.SelectionPanel;
using UnityEngine;

namespace Mono.StateMachine
{
    public class StateMachineUIDependencies : MonoBehaviour
    {
        [field: SerializeField] public SelectionPanel SelectionPanel {  get; private set; }
        [field: SerializeField] public MissionPreparationUI MissionPreparationUI { get; private set; }
        [field: SerializeField] public MissionExecutionTimer MissionExecutionUI { get; private set; }
        [field: SerializeField] public ResultPanel ResultPanelHolder { get; private set; }
    }
}