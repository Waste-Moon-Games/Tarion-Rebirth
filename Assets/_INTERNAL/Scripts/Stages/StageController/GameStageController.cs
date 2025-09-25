using Core.Common;
using Core.Factories;
using StateMachine.Base;
using System;
using UnityEngine;
using IDisposable = Core.Common.IDisposable;

namespace Stages.StageController
{
    public class GameStageController : IGameStageController
    {
        private readonly IStageFactory _factory;
        private readonly int _controllerId;
        private IStage _currentStage;
        private bool _isRunning = false;

        public IStageFactory StageFactory { get { return _factory; } }
        public bool HasInactive => _isRunning != true;
        public int ControllerId => _controllerId;

        public event Action OnMissionStarted;
        public event Action OnResultAccepted;

        public GameStageController(IStageFactory factory, int controllerId)
        {
            _factory = factory;
            _controllerId = controllerId;
        }

        public void StartCycle()
        {
            _isRunning = true;
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
            _isRunning = false;
            _currentStage?.Exit();
            (_currentStage as IDisposable)?.Dispose();
            _currentStage = null;

            OnResultAccepted?.Invoke();
            _factory.OnMissionExecutionStageCreated -= HandleCreatedMissionExecutionStage;
        }

        public void ForceEnd()
        {
            _isRunning = false;
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