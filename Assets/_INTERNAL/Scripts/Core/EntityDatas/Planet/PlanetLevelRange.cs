namespace Core.EntityDatas.Planet
{
    [System.Serializable]
    public struct PlanetLevelRange
    {
        public string ThreatName;

        public int MinLevel;
        public int MaxLevel;
    }

    [System.Serializable]
    public struct PlanetAttributes<T>
    {
        public string Name;
        public T MinValue;
        public T MaxValue;
    }
}