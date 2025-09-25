using Core.EntityDatas.Unit;
using System.Collections.Generic;
using UnityEngine;
using Utils.Reader;

namespace Core.EntityGenerationConfigs
{
    [System.Serializable]
    public class HeroGenerationConfig
    {
        [Header("Data Files")]
        public TextAsset MaleNamesFile;
        public TextAsset FemaleNamesFile;
        public TextAsset TemplatesFile;
        public TextAsset FromHeroFile;
        public TextAsset GenderHeroFile;

        [Header("General Info")]
        public List<string> MaleNameTemplates;
        public List<string> FemaleNameTemplates;
        public List<string> Templates;
        public List<string> FromHeroNameTemplates;
        public List<string> GenderHeroTemplates;
        public Sprite HeroArt;

        [Header("Attributes")]
        public List<HeroAttributes<int>> HeroAttributes;
        public List<HeroAttributes<float>> AttributesMultipliers;
        public List<HeroAttributes<float>> HeroBasePower;
        public List<HeroAttributes<float>> HeroPowerGrowthMultiplier;

        public void LoadNames()
        {
            List<string> maleNames = NamesReader.LoadFromTextAsset(MaleNamesFile);
            List<string> femaleNames = NamesReader.LoadFromTextAsset(FemaleNamesFile);

            MaleNameTemplates.Clear();
            FemaleNameTemplates.Clear();

            MaleNameTemplates.AddRange(maleNames);
            FemaleNameTemplates.AddRange(femaleNames);
        }

        public void LoadTemplates()
        {
            List<string> templates = NamesReader.LoadFromTextAsset(TemplatesFile);

            Templates.Clear();

            Templates.AddRange(templates);
        }

        public void LoadFromTemplates()
        {
            List<string> fromTemplates = NamesReader.LoadFromTextAsset(FromHeroFile);

            FromHeroNameTemplates.Clear();

            FromHeroNameTemplates.AddRange(fromTemplates);
        }

        public void LoadGenderHeroTemplates()
        {
            List<string> genderTemplates = NamesReader.LoadFromTextAsset(GenderHeroFile);

            GenderHeroTemplates.Clear();

            GenderHeroTemplates.AddRange(genderTemplates);
        }
    }
}