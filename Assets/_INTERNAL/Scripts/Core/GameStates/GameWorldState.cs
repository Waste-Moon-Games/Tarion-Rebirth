using GameEntity.ScriptableObjects;
using SO.Containers.GameEntity;
using System.Collections.Generic;
using UnityEngine;

namespace Core.GameStates
{
    public class GameWorldState
    {
        [field: SerializeField] public ImperiumState ImperiumState { get; private set; }

        public GameWorldState(List<HeroDataContainer> heroDatas, List<PlanetDataContainer> planetDatas, List<MissionDataContainer> missionDatas, RankProgressionConfig progressionConfig)
        {
            ImperiumState = new ImperiumState(heroDatas, planetDatas, missionDatas, progressionConfig);
        }
    }
}