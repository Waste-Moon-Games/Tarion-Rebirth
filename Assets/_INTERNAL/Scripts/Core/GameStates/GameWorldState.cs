using GameEntity.ScriptableObjects;
using SO.Containers.Configs;
using SO.Containers.GameEntity;
using System.Collections.Generic;
using UnityEngine;

namespace Core.GameStates
{
    public class GameWorldState
    {
        [field: SerializeField] public ImperiumState ImperiumState { get; private set; }
        [field: SerializeField] public ImperiumStateController ImperiumStateController { get; private set; }

        public GameWorldState
            (List<HeroDataContainer> heroDatas,
            List<PlanetDataContainer> planetDatas,
            List<MissionDataContainer> missionDatas,
            RankProgressionConfig progressionConfig,
            ImperiumConfig limitsConfig)
        {
            ImperiumState = new(heroDatas,planetDatas,missionDatas,progressionConfig,limitsConfig);

            ImperiumStateController = new(ImperiumState);
        }
    }
}