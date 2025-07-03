using GameEntity.DataInstance.Main;
using Scripts.Mono.UI.PlanetListUI;
using UnityEngine;

namespace Mono.UI.PlanetListUI
{
    public class PlanetListController : MonoBehaviour
    {
        [SerializeField] private Transform _contentParent;
        [SerializeField] private PlanetViewItem _planetItemPrefab;

        private InstanceHolder _instanceHolder;

        public void Initialize(InstanceHolder instanceHolder)
        {
            _instanceHolder = instanceHolder;
            GeneratePlanetList();
        }

        private void GeneratePlanetList()
        {
            foreach (var planet in _instanceHolder.Planets)
            {
                var itemGO = Instantiate(_planetItemPrefab, _contentParent);
                var item = itemGO.GetComponent<PlanetViewItem>();
                item.Setup(planet);
            }
        }
    }
}