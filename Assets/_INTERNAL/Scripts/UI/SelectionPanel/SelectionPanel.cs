using UI.Base;
using UI.HeroMenu.Views;
using UI.MissionContexUI;
using UI.PlanetListUI;
using UnityEngine;

namespace UI.SelectionPanel
{
    public class SelectionPanel : SimpleUIItem
    {
        [field: SerializeField] public TargetPlanetsListController PlanetListController { get; private set; }
        [field: SerializeField] public AvailableHeroListView HeroListController { get; private set; }
        [field: SerializeField] public MissionTypeListController MissionTypeListController { get; private set; }
    }
}