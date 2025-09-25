using GameEntity.Unit.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SO.Containers.GameEntity
{
    [Serializable]
    public class RankProgressionEntry
    {
        public Rank Rank;
        public int RequiredLevel;
    }

    [CreateAssetMenu(menuName = "Config containers/HeroRank config", fileName = "HeroRank cofig")]
    public class RankProgressionConfig : ScriptableObject
    {
        [SerializeField] private List<RankProgressionEntry> _entries;

        public Rank? GetNextRank(int currentLevel, Rank currentRank)
        {
            foreach (var entry in _entries)
            {
                if (entry.RequiredLevel <= currentLevel && entry.Rank > currentRank)
                    return entry.Rank;
            }

            return null;
        }
    }
}