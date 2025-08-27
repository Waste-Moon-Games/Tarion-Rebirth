using Mono.InstanceInitialize;
using UI.HeroDetailInfoUI;
using UI.PlanetsMap;
using UnityEngine;

namespace Entry
{
    public class DataHolder : MonoBehaviour
    {
        [Header("Datas")]
        [SerializeField] private BootUniqueDatas _bootDatas;

        [Space(10), Header("UIs")]
        [SerializeField] private HeroInfoHolder _heroInfoHolder;
        [SerializeField] private GalaxyMapController _galaxyMapController;

        public BootUniqueDatas BootDatas => _bootDatas;

        private void Awake()
        {
            _bootDatas.BootGameData();
            _heroInfoHolder.SetInstanceHolder(_bootDatas.InstanceHolder);
            _galaxyMapController.SetInstanceHolder(_bootDatas.InstanceHolder);
        }
    }
}