using StateMachine.Base;
using UnityEngine;

namespace StateMachine
{
    public class GameBootstrap : MonoBehaviour
    {
        private GameStageController _stageController;

        private void Start()
        {
            _stageController = new();

            _stageController.Start();
        }

        private void Update()
        {
            _stageController?.Update();
        }
    }
}