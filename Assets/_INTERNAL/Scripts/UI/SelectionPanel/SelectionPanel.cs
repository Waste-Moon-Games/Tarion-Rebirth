using Mono.UI.HeroListUI;
using Mono.UI.MissionContexUI;
using Mono.UI.PlanetListUI;
using UI.Base;
using UnityEngine;

namespace UI.SelectionPanel
{
    public class SelectionPanel : SimpleUIItem
    {
        [field: SerializeField] public TargetPlanetsListController PlanetListController { get; private set; }
        [field: SerializeField] public HeroListController HeroListController { get; private set; }
        [field: SerializeField] public MissionTypeListController MissionTypeListController { get; private set; }
    }
}