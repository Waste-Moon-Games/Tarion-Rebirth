using GameEntity.Unit.Data;
using SO.Containers.GameEntity;
using System;
using UnityEngine;

namespace Core.GrowthSystem
{
    public class HeroRankUpSystem
    {
        private readonly HeroRuntimeData _data;
        private readonly RankProgressionConfig _config;

        public event Action<Rank> OnRankLevelUp;

        public HeroRankUpSystem(HeroRuntimeData data, RankProgressionConfig config)
        {
            _data = data;
            _config = config;
        }

        public void OnHeroLevelChanged(int newLevel)
        {
            var nextRank = _config.GetNextRank(newLevel, _data.Rank);

            if (nextRank.HasValue)
            {
                _data.Rank = nextRank.Value;
                Debug.Log($"{_data.Name} is a {_data.Rank}");
                OnRankLevelUp?.Invoke(nextRank.Value);
            }
        }

        public Rank GetCurrentRank() => _data.Rank;
    }
}