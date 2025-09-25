using UnityEngine;

namespace SO.Containers.Configs
{
    [CreateAssetMenu(menuName = "Config containers/Imperium Magna/Config", fileName = "ImperiumConfig")]
    public class ImperiumConfig : ScriptableObject
    {
        [Header("Limits")]
        [field: SerializeField, Range(1, 50)] public int StartMaxPlanetsLimit { get; private set; }
        [field: SerializeField, Range(1, 50)] public int StartMaxHerosLimit { get; private set; }
        [field: SerializeField] public int MissionSlots { get; private set; }

        [Header("Resources state")]
        [field: SerializeField] public float StartExtractionTime {  get; private set; }
        [field: SerializeField, Range(0.5f, 5f)] public float CapturedResourcesMultiplier { get; private set; }
    }
}