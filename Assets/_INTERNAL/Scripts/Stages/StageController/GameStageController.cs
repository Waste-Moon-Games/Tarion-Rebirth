using Core.Common;
using Core.Factories;
using Mono.UI;
using StateMachine.Base;
using System;
using UI.Base;
using UnityEngine;
using IDisposable = Core.Common.IDisposable;

namespace Stages.StageController
{
    public class GameStageController : IGameStageController
    {
        private readonly IStageFactory _factory;
        private IStage _currentStage;

        public IStageFactory StageFactory { get { return _factory; } }

        public event Action OnMissionStarted;
        public event Action OnResultAccepted;

        public GameStageController(IStageFactory factory)
        {
            _factory = factory;
        }

        public void StartCycle()
        {
            SetStage(_factory.CreatePlanetSelectionStage(this));
            _factory.OnMissionExecutionStageCreated += HandleCreatedMissionExecutionStage;
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

        public void EndCycle()
        {
            _currentStage?.Exit();
            (_currentStage as IDisposable)?.Dispose();
            _currentStage = null;

            OnResultAccepted?.Invoke();
            _factory.OnMissionExecutionStageCreated -= HandleCreatedMissionExecutionStage;
        }

        public void ForceEnd()
        {
            _currentStage?.Exit();
            (_currentStage as IDisposable)?.Dispose();
            _currentStage = null;
            _factory.OnMissionExecutionStageCreated -= HandleCreatedMissionExecutionStage;
        }

        public void Update()
        {
            _currentStage?.Tick();
        }

        public void RefreshDeps(IDependence dependence)
        {
            _currentStage?.RefreshDeps(dependence);
        }

        private void HandleCreatedMissionExecutionStage()
        {
            OnMissionStarted?.Invoke();
        }
    }
}