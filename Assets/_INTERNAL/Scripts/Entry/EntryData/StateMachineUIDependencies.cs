using Mono.UI.HeroListUI;
using Mono.UI.MissionContexUI;
using Mono.UI.PlanetListUI;
using UI.MissionContexUI;
using UI.MissionExecutionUI;
using UnityEngine;

namespace Mono.StateMachine
{
    public class StateMachineUIDependencies : MonoBehaviour
    {
        [field: SerializeField] public PlanetListController PlanetListController { get; private set; }
        [field: SerializeField] public HeroListController HeroListController { get; private set; }
        [field: SerializeField] public MissionTypeListController MissionTypeListController { get; private set; }
        [field: SerializeField] public MissionPreparationUI MissionPreparationUI { get; private set; }
        [field: SerializeField] public MissionExecutionTimer MissionExecutionUI { get; private set; }
    }
}