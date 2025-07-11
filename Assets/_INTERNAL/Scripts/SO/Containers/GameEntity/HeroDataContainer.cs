using GameEntity.Unit.Data;
using UnityEngine;

namespace GameEntity.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Data Containers/Hero", fileName = "Hero Container")]
    public class HeroDataContainer : ScriptableObject
    {
        [field: SerializeField] public HeroData HeroData { get; private set; }
    }
}