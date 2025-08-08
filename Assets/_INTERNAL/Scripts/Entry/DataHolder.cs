using Mono.InstanceInitialize;
using UI.HeroDetailInfoUI;
using UnityEngine;

namespace Entry
{
    public class DataHolder : MonoBehaviour
    {
        [Header("Datas")]
        [SerializeField] private BootDatas _bootDatas;

        [Space(10), Header("UIs")]
        [SerializeField] private HeroInfoHolder _heroInfoHolder;

        public BootDatas BootDatas => _bootDatas;

        private void Awake()
        {
            _bootDatas.BootGameData();
            _heroInfoHolder.SetInstanceHolder(_bootDatas.InstanceHolder);
        }
    }
}