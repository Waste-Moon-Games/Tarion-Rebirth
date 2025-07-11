using GameEntity.DataInstance.Main;
using GameEntity.ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;

namespace Mono.InstanceInitialize
{
    public class BootDatas : MonoBehaviour
    {
        [field:SerializeField] public List<HeroDataContainer> HeroDatas { get; private set; }
        [field: SerializeField] public List<PlanetDataContainer> PlanetDatas { get; private set; }
        [field: SerializeField] public List<MissionDataContainer> MissionDatas { get; private set; }

        [field: SerializeField] public InstanceHolder InstanceHolder { get; private set; }

        public void BootGameData()
        {
            InstanceHolder = new();

            InstanceHolder.Initialize(HeroDatas, PlanetDatas, MissionDatas);
        }
    }
}