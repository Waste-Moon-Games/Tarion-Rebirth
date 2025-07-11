using GameEntity.Planet;
using UnityEngine;

namespace GameEntity.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Data Containers/Planet", fileName = "Planet Container")]
    public class PlanetDataContainer : ScriptableObject
    {
        [field: SerializeField] public PlanetData PlanetData { get; private set; }
    }
}