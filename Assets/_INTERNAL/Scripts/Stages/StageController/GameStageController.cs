using Core.Factories;
using StateMachine.Base;
using UnityEngine;
using System;
using IDisposable = Core.Common.IDisposable;

namespace Stages.StageController
{
    public class GameStageController : IGameStageController
    {
        private readonly IStageFactory _factory;
        private IStage _currentStage;

        public IStageFactory StageFactory { get { return _factory; } }

        public event Action OnResultAccepted;

        public GameStageController(IStageFactory factory)
        {
            _factory = factory;
        }

        public void Start()
        {
            SetStage(_factory.CreatePlanetSelectionStage(this));
        }

        public void SetStage(IStage newStage)
        {
            if (_currentStage == newStage)
                return;

            _currentStage?.Exit();
            (_currentStage as IDisposable)?.Dispose();
            _currentStage = newStage;
            _currentStage.Enter();
        }

        public void ExitStage()
        {
            _currentStage?.Exit();
            (_currentStage as IDisposable)?.Dispose();

            OnResultAccepted?.Invoke();
        }

        public void Update()
        {
            _currentStage?.Tick();
        }
    }
}