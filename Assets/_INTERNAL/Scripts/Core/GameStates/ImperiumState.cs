using GameEntity.DataInstance.Main;
using GameEntity.ScriptableObjects;
using SO.Containers.GameEntity;
using System.Collections.Generic;
using UnityEngine;

namespace Core.GameStates
{
    public class ImperiumState
    {
        [field: SerializeField] public TargetsListState TargetsListState { get; private set; }
        [field: SerializeField] public ImperiumInstancesHolder InstanceHolder { get; private set; }

        public ImperiumState(List<HeroDataContainer> heroDatas, List<PlanetDataContainer> planetDatas, List<MissionDataContainer> missionDatas, RankProgressionConfig progressionConfig)
        {
            InstanceHolder = new();
            InstanceHolder.Initialize(heroDatas, planetDatas, missionDatas, progressionConfig);

            TargetsListState = new(InstanceHolder);
        }


    }
}