using Mono.InstanceInitialize;
using Mono.UI.PlanetListUI;
using UnityEngine;

namespace StateMachine
{
    public class GameBootstrap : MonoBehaviour
    {
        [Header("Datas")]
        [SerializeField] private BootDatas _bootDatas;

        [Space(5), Header("UI")]
        [SerializeField] private PlanetListController _planetListController;

        private GameStageController _stageController;

        private void Start()
        {
            _bootDatas.BootGameData();

            _planetListController.Initialize(_bootDatas.InstanceHolder);
        }
    }
}