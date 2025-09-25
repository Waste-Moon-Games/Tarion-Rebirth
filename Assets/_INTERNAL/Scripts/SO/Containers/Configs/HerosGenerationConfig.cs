using Core.EntityGenerationConfigs;
using UnityEngine;

namespace SO.Containers.Configs
{
    [CreateAssetMenu(menuName = "Config containers/Generation/Heros", fileName = "HerosGenerationConfig")]
    public class HerosGenerationConfig : ScriptableObject
    {
        [field: SerializeField] public HeroGenerationConfig Config { get; private set; }
        [field: SerializeField, Range(1, 10)] public int Count { get; private set; }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (Config.MaleNameTemplates != null)
                Config.LoadNames();

            if (Config.Templates != null)
                Config.LoadTemplates();

            if (Config.FromHeroNameTemplates != null)
                Config.LoadFromTemplates();

            if(Config.GenderHeroTemplates != null)
                Config.LoadGenderHeroTemplates();
        }
#endif
    }
}