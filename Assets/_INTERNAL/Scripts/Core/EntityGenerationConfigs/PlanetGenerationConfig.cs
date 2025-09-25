using Core.EntityDatas.Planet;
using Core.EntityDatas.Resource;
using GameEntity.Planet;
using System.Collections.Generic;
using UnityEngine;
using Utils.Reader;

namespace Core.EntityGenerationConfigs
{
    [System.Serializable]
    public class PlanetGenerationConfig
    {
        [Header("Data Files")]
        public TextAsset NamesFile;
        public TextAsset TypesFile;
        public TextAsset RacesFile;
        public TextAsset FeaturesFile;
        public TextAsset TemplatesFile;

        [Header("General Info")]
        public List<string> NameTemplates;
        public List<string> TypesTemplates;
        public List<string> RacesTemplates;
        public List<string> FeaturesTemplates;
        public List<string> Templates;
        public PlanetType PlanetType;

        [Header("Population")]
        public List<PlanetAttributes<int>> PopulationRange;

        [Header("Tech Power")]
        public List<PlanetAttributes<float>> BaseTechPowerRange;
        public List<PlanetAttributes<float>> TechPowerMultipliersRange;

        [Header("Resistance")]
        public List<PlanetAttributes<float>> BaseResistanceRange;
        public List<PlanetAttributes<float>> ResistanceMultipliersRange;

        [Header("Resources")]
        public List<PlanetAttributes<int>> ResourcesRange;
        public List<ResourceType> ResourceTypes;

        [Header("Planet Level")]
        public int LowLevelPlanetCount;
        public int MidLevelPlanetCount;
        public int HighLevelPlanetCount;
        public List<PlanetLevelRange> LevelRange;

        public void LoadNames()
        {
            List<string> names = NamesReader.LoadFromTextAsset(NamesFile);
            NameTemplates.Clear();

            NameTemplates.AddRange(names);
        }

        public void LoadTypes()
        {
            List<string> types = NamesReader.LoadFromTextAsset(TypesFile);
            TypesTemplates.Clear();

            TypesTemplates.AddRange(types);
        }

        public void LoadRaces()
        {
            List<string> races = NamesReader.LoadFromTextAsset(RacesFile);
            RacesTemplates.Clear();

            RacesTemplates.AddRange(races);
        }

        public void LoadFeatures()
        {
            List<string> features = NamesReader.LoadFromTextAsset(FeaturesFile);
            FeaturesTemplates.Clear();

            FeaturesTemplates.AddRange(features);
        }

        public void LoadTemplates()
        {
            List<string> templates = NamesReader.LoadFromTextAsset(TemplatesFile);
            Templates.Clear();

            Templates.AddRange(templates);
        }
    }
}