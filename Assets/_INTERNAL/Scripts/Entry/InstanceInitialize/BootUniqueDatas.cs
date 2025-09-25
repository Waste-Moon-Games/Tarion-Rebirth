using GameEntity.ScriptableObjects;
using SO.Containers.Configs;
using SO.Containers.GameEntity;
using System.Collections.Generic;
using UnityEngine;

namespace Mono.InstanceInitialize
{
    public class BootUniqueDatas : MonoBehaviour
    {
        [field:SerializeField] public List<HeroDataContainer> HeroDatas { get; private set; }
        [field: SerializeField] public List<PlanetDataContainer> PlanetDatas { get; private set; }
        [field: SerializeField] public List<MissionDataContainer> MissionDatas { get; private set; }
        [field: SerializeField] public RankProgressionConfig RankProgressionConfig { get; private set; }
        [field: SerializeField] public ImperiumConfig ImperiumConfig { get; private set; }
    }
}