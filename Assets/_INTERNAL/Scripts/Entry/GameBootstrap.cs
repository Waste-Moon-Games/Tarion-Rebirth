using Mono.InstanceInitialize;
using UnityEngine;

namespace StateMachine
{
    public class GameBootstrap : MonoBehaviour
    {
        [Header("Datas")]
        [SerializeField] private BootDatas _bootDatas;

        private void Start()
        {
            _bootDatas.BootGameData();
        }
    }
}