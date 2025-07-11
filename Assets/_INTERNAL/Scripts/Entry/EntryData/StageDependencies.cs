using Contex.MissionInfo;
using GameEntity.DataInstance.Main;
using Mono.StateMachine;
using UnityEngine;

namespace Core.Factories.Stage_Factory
{
    public class StageDependencies
    {
        [field: SerializeField] public InstanceHolder InstanceHolder { get; private set; }
        [field: SerializeField] public MissionContex MissionContex { get; private set; }
        [field: SerializeField] public StateMachineUIDependencies UIDependencies { get; private set; }

        public StageDependencies(InstanceHolder holder, MissionContex contex, StateMachineUIDependencies dependencies)
        {
            InstanceHolder = holder;
            MissionContex = contex;
            UIDependencies = dependencies;

        }
    }
}