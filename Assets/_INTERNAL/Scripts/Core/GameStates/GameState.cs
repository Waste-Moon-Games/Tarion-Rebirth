using GameEntity.ScriptableObjects;
using SO.Containers.Configs;
using SO.Containers.GameEntity;
using System.Collections.Generic;
using UnityEngine;
using Utils.ModCoroutines;

namespace Core.GameStates
{
    public class GameState
    {
        [field: SerializeField] public ImperiumState ImperiumState { get; private set; }
        [field: SerializeField] public ImperiumStateController ImperiumStateController { get; private set; }

        public GameState
            (List<HeroDataContainer> heroDatas,
            List<PlanetDataContainer> planetDatas,
            List<MissionDataContainer> missionDatas,
            RankProgressionConfig progressionConfig,
            ImperiumConfig limitsConfig,
            Coroutines coroutines)
        {
            ImperiumState = new(heroDatas,planetDatas,missionDatas,progressionConfig,limitsConfig, coroutines);

            ImperiumStateController = new(ImperiumState);
        }
    }
}