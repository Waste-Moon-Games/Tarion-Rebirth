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
        public int MinPopulation;
        public int MaxPopulation;

        [Header("Tech Power")]
        public float MinBaseTechPower;
        public float MaxBaseTechPower;
        public float MinTechMultiplier;
        public float MaxTechMultiplier;

        [Header("Resistance")]
        public float MinBaseResistance;
        public float MaxBaseResistance;
        public float MinResistanceMultiplier;
        public float MaxResistanceMultiplier;

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