using UnityEngine;

namespace GameEntity.Planet
{
    [System.Serializable]
    public struct PlanetData
    {
        public string PlanetName;
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
}
