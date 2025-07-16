using UnityEngine;

namespace GameEntity.Planet
{
    [System.Serializable]
    public class PlanetData
    {
        public string PlanetName;

        [TextArea]
        public string PlanetDescription;

        [Space(5)]
        public int Population;
        public float PlanetPower;

        [Space(5)]
        public float BaseTechPower;
        public float TechPower;
        public float TechMultiplier;

        [Space(5)]
        public float BaseResistance;
        public float Resistance;
        public float ResistanceMultiplier;

        [Space(5)]
        public PlanetType Type;

        [Space(5)]
        public bool IsCaptured;
    }

    public class PlanetRuntimeData
    {
        public string PlanetName;

        public string PlanetDescription;

        public int Population;
        public float PlanetPower;

        public float BaseTechPower;
        public float TechPower;
        public float TechMultiplier;

        public float BaseResistance;
        public float Resistance;
        public float ResistanceMultiplier;

        public PlanetType Type;

        public bool IsCaptured;

        public PlanetRuntimeData(PlanetData source)
        {
            PlanetName = source.PlanetName;
            PlanetDescription = source.PlanetDescription;

            Population = source.Population;
            PlanetPower = source.PlanetPower;

            BaseTechPower = source.BaseTechPower;
            TechPower = source.TechPower;
            TechMultiplier = source.TechMultiplier;

            BaseResistance = source.BaseResistance;
            Resistance = source.Resistance;
            ResistanceMultiplier = source.ResistanceMultiplier;

            Type = source.Type;
            IsCaptured = source.IsCaptured;
        }
    }
}