using StateMachine.Base;
using StateMachine.Stages;
using UnityEngine;

namespace StateMachine
{
    public class GameStageController : IGameStageController
    {
        private IStage _currentStage;

        public void SetStage(IStage newStage)
        {
            if (_currentStage == newStage)
            {
                Debug.Log($"Stage already settled: {newStage}");
                return;
            }

            _currentStage?.Exit();
            _currentStage = newStage;
            _currentStage.Enter();
        }

        public void Start()
        {
            SetStage(new PlanetSelectionStage(this));
        }

        public void Update()
        {
            _currentStage?.Tick();
        }
    }
}