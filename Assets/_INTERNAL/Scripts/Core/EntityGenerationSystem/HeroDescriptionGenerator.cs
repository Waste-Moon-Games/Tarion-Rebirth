using Core.EntityGenerationConfigs;
using SO.Containers.Configs;
using System.Collections.Generic;
using UnityEngine;

namespace Core.EntityGenerationSystem
{
    public class HeroDescriptionGenerator
    {
        private readonly HeroGenerationConfig _config;

        public HeroDescriptionGenerator(HerosGenerationConfig config)
        {
            _config = config.Config;
        }

        public string GenerateDescription()
        {
            string template = GetRandom(_config.Templates);

            return template
                .Replace("{from}", GetRandom(_config.FromHeroNameTemplates))
                .Replace("{gender}", GetRandom(_config.GenderHeroTemplates))
                .Replace("{name}", GetRandom(_config.MaleNameTemplates));
        }

        private string GetRandom(List<string> list)
        {
            if (list == null || list.Count == 0) return "???";

            return list[Random.Range(0, list.Count)];
        }
    }
}
