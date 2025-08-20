using Core.EntityGenerationConfigs;
using System.Collections.Generic;
using UnityEngine;

namespace Core.EntityGenerateSystem
{
    public class PlanetDescriptionGenerator
    {
        private readonly PlanetGenerationConfig _config;

        public PlanetDescriptionGenerator(PlanetGenerationConfig config)
        {
            _config = config;
        }

        public string GenerateDescription()
        {
            string template = GetRandom(_config.Templates);

            return template
                .Replace("{type}", GetRandom(_config.TypesTemplates))
                .Replace("{race}", GetRandom(_config.RacesTemplates))
                .Replace("{feature}", GetRandom(_config.FeaturesTemplates));
        }

        private string GetRandom(List<string> list)
        {
            if (list == null || list.Count == 0) return "???";

            return list[Random.Range(0, list.Count)];
        }
    }
}