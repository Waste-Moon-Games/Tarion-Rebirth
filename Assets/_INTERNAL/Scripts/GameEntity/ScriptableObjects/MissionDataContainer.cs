using GameEntity.Mission;
using UnityEngine;

namespace GameEntity.ScriptableObjects
{
    [CreateAssetMenu(menuName = "Data Containers/Mission", fileName = "Mission Container")]
    public class MissionDataContainer : ScriptableObject
    {
        [field: SerializeField] public MissionData MissionData { get; private set; }
    }
}