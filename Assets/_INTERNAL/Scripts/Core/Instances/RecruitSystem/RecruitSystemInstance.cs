using GameEntity.Unit.Data;
using Scripts.GameEntity.DataInstance;
using SO.Containers.GameEntity;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Instances.RecruitSystem
{
    public class RecruitSystemInstance
    {
        [field: SerializeField] public List<HeroInstance> Heros { get; private set; } = new();
        [field: SerializeField] public RankProgressionConfig Config { get; private set; }

        public RecruitSystemInstance(RankProgressionConfig config) => Config = config;

        public void SetGeneratedHeros(List<HeroData> generatedHeros)
        {
            Heros.Clear();

            foreach (HeroData data in generatedHeros)
            {
                Heros.Add(new(data, Config));
            }
        }
    }
}