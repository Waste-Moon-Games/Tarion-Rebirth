using Contex.MissionInfo;
using Core.Common;
using Core.GameStates;
using GameEntity.DataInstance.Main;
using Mono.StateMachine;
using UnityEngine;

namespace Entry.EntryData
{
    public class StageDependencies : IDependence
    {
        [field: SerializeField] public TargetsListState TargetsList { get; private set; }
        [field: SerializeField] public ImperiumInstancesHolder InstanceHolder { get; private set; }
        [field: SerializeField] public MissionContex MissionContex { get; private set; }
        [field: SerializeField] public StateMachineUIDependencies UIDependencies { get; private set; }

        public StageDependencies(ImperiumInstancesHolder holder, MissionContex contex, StateMachineUIDependencies dependencies, TargetsListState targetsList)
        {
            TargetsList = targetsList;
            InstanceHolder = holder;
            MissionContex = contex;
            UIDependencies = dependencies;
        }

        public void RefreshUIDependencies(StateMachineUIDependencies dependencies)
        {
            UIDependencies = dependencies;
        }
    }
}