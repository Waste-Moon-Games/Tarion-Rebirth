using Core.EntityGenerationConfigs;
using UnityEngine;

namespace SO.Containers.Configs
{
    [CreateAssetMenu(menuName = "Config containers/Generation/Planets", fileName = "PlanetsGenerationConfig")]
    public class PlanetsGenerationConfig : ScriptableObject
    {
        [field: SerializeField] public PlanetGenerationConfig PlanetsConfig {  get; private set; }
        [field: SerializeField, Range(1, 50)] public int PlanetCount { get; private set; }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if(PlanetsConfig.NameTemplates != null)
                PlanetsConfig.LoadNames();

            if (PlanetsConfig.TypesTemplates != null)
                PlanetsConfig.LoadTypes();

            if (PlanetsConfig.RacesTemplates != null)
                PlanetsConfig.LoadRaces();

            if(PlanetsConfig.FeaturesTemplates != null)
                PlanetsConfig.LoadFeatures();

            if(PlanetsConfig.Templates != null)
                PlanetsConfig.LoadTemplates();

            PlanetCount = PlanetsConfig.LowLevelPlanetCount + PlanetsConfig.MidLevelPlanetCount + PlanetsConfig.HighLevelPlanetCount;
        }
#endif
    }
}