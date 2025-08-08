using GameEntity.Unit.Data;
using System;

namespace Core.Common
{
    public interface IStatsUpgradeView
    {
        event Action OnStrengthClicked;
        event Action OnDexterityClicked;
        event Action OnIntelligenceClicked;
        void SetSkillPoints(int value);
        void SetStats(HeroStatsRuntime stats);
    }
}