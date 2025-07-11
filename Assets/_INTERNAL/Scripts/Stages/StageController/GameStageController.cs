using Core.Factories;
using StateMachine.Base;
using UnityEngine;

namespace Stages.StageController
{
    public class GameStageController : IGameStageController
    {
        private IStage _currentStage;
        private IStageFactory _factory;

        public IStageFactory StageFactory { get { return _factory; } }

        public GameStageController(IStageFactory factory)
        {
            _factory = factory;
        }

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
            SetStage(_factory.CreatePlanetSelectionStage(this));
        }

        public void Update()
        {
            _currentStage?.Tick();
        }
    }
}