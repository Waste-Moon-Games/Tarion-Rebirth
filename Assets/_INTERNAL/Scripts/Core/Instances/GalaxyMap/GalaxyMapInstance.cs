using GameEntity.DataInstance;
using GameEntity.Planet;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Instances.GalaxyMap
{
    public class GalaxyMapInstance
    {
        [field: SerializeField] public List<PlanetInstance> Planets { get; private set; } = new();

        public void GetGeneratedPlanets(List<PlanetData> planetsData)
        {
            Planets.Clear();

            foreach (PlanetData data in planetsData)
            {
                Planets.Add(new(data));
            }
        }
    }
}