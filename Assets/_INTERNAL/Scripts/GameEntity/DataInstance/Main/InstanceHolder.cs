using GameEntity.ScriptableObjects;
using Scripts.GameEntity.DataInstance;
using System.Collections.Generic;
using UnityEngine;

namespace GameEntity.DataInstance.Main
{
    public class InstanceHolder
    {
        [field: SerializeField] public List<HeroInstance> Heros { get; private set; } = new();
        [field: SerializeField] public List<PlanetInstance> Planets { get; private set; } = new();
        //[field: SerializeField] public List<MissionInstance> Missions { get; private set;} = new();

        public void Initialize(List<HeroDataContainer> heroDatas,
            List<PlanetDataContainer> planetDatas, List<MissionDataContainer> missionDatas)
        {
            InitializeHeros(heroDatas);
            InitializePlanets(planetDatas);
            //InitializeMissions(missionDatas);
        }

        private void InitializeHeros(List<HeroDataContainer> heroDatas)
        {
            Heros.Clear();

            foreach (HeroDataContainer data in heroDatas)
            {
                Heros.Add(new(data));
            }
        }

        private void InitializePlanets(List<PlanetDataContainer> planetDatas)
        {
            Planets.Clear();

            foreach (PlanetDataContainer data in planetDatas)
            {
                Planets.Add(new(data));
            }
        }

        //private void InitializeMissions(List<MissionDataContainer> missionDatas)
        //{
        //    Missions.Clear();

        //    foreach (MissionDataContainer data in missionDatas)
        //    {
        //        Missions.Add(new(data));
        //    }
        //}
    }
}