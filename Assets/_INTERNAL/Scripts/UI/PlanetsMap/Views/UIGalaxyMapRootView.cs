using UI.Base;
using UnityEngine;

namespace UI.PlanetsMap
{
    public class UIGalaxyMapRootView : UIRootView
    {
        [field: SerializeField] public Transform SpawnArea { get; private set; }
    }
}