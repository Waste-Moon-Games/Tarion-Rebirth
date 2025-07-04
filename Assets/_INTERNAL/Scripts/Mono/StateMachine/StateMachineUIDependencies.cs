using Mono.UI.HeroListUI;
using Mono.UI.MissionContexUI;
using Mono.UI.PlanetListUI;
using UnityEngine;

namespace Mono.StateMachine
{
    public class StateMachineUIDependencies : MonoBehaviour
    {
        [field: SerializeField] public PlanetListController PlanetListController { get; private set; }
        [field: SerializeField] public HeroListController HeroListController { get; private set; }
        [field: SerializeField] public MissionTypeListController MissionTypeListController { get; private set; }
    }
}