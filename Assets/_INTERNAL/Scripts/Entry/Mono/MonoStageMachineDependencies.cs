using Entry.Mono.MissionPanel;
using Mono.StateMachine;
using System;
using UnityEngine;

namespace Entry.Mono
{
    public class MonoStageMachineDependencies : MonoBehaviour
    {
        public static event Action<MonoStageMachineDependencies> OnSceneDependenciesReady;

        private static MonoStageMachineDependencies _current;
        public static MonoStageMachineDependencies Current => _current;

        [SerializeField] private StateMachineUIDependencies _uiDependencies;
        [SerializeField] private StartPrepareMission _startPrepareMission;

        public StateMachineUIDependencies StateMachineUIDependencies => _uiDependencies;
        public StartPrepareMission StartPrepareMission => _startPrepareMission;

        private void Awake()
        {
            _current = this;
            OnSceneDependenciesReady?.Invoke(this);
        }

        private void OnDestroy()
        {
            if (_current == this)
                _current = null;
        }
    }
}