using UnityEngine;

namespace SO.Containers.Configs
{
    [CreateAssetMenu(menuName = "Config containers/Limits/Imperium Instances Holder", fileName = "ImperuimInstancesStartLimitsConfig")]
    public class ImperuimInstancesStartLimitsConfig : ScriptableObject
    {
        [field: SerializeField, Range(1, 50)] public int StartMaxPlanetsLimit { get; private set; }
        [field: SerializeField, Range(1, 50)] public int StartMaxHerosLimit { get; private set; }
    }
}
