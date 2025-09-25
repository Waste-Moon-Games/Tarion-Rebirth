using Core.EntityDatas.Unit.Data;
using UnityEngine;

namespace Core.EntityDatas.Unit
{
    [System.Serializable]
    public struct HeroAttributes<T>
    {
        public HeroQuality Quality;
        [Range(0.01f, 1f)] public float DropChance;
        public T MinValue;
        public T MaxValue;
    }
}
