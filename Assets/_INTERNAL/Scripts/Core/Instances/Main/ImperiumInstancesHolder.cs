using Core.Common.Instances;
using GameEntity.Mission;
using GameEntity.ScriptableObjects;
using Scripts.GameEntity.DataInstance;
using SO.Containers.Configs;
using SO.Containers.GameEntity;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameEntity.DataInstance.Main
{
    public class ImperiumInstancesHolder
    {
        [field: SerializeField] public List<HeroInstance> Heros { get; private set; } = new();
        [field: SerializeField] public List<PlanetInstance> Planets { get; private set; } = new();
        [field: SerializeField] public List<MissionType> Missions { get; private set; } = new();

        private int _maxPlanets;
        private int _maxHeros;

        public bool HasAvailablePositionInPlanetsList => Planets.Count < _maxPlanets;
        public int MaxPlanets => _maxPlanets;
        public int MaxHeros => _maxHeros;

        public event Action<PlanetInstance> OnPlanetAdded;
        public event Action<int> OnPlanetsCountChanged;
        public event Action<PlanetInstance> OnPlanetRejected;
        public event Action<int> OnPlanetsLimitUpgraded;

        public event Action<HeroInstance> OnHerosListUpdated;

        public ImperiumInstancesHolder(ImperuimInstancesStartLimitsConfig limitsConfig)
        {
            _maxPlanets = limitsConfig.StartMaxPlanetsLimit;
            _maxHeros = limitsConfig.StartMaxHerosLimit;
        }

        public void Initialize
            (
            List<HeroDataContainer> heroDatas,
            List<PlanetDataContainer> planetDatas,
            List<MissionDataContainer> missionDatas,
            RankProgressionConfig config
            )
        {
            InitializeHeros(heroDatas, config);
            InitializePlanets(planetDatas);
            InitializeAvailableTypesOfMissions(missionDatas);
        }

        public void AddCapturedPlanet(IInstance planet)
        {
            var newPlanet = planet as PlanetInstance;

            if(Planets.Count >= _maxPlanets)
            {
                OnPlanetRejected?.Invoke(newPlanet);
                return;
            }

            if (!Planets.Contains(newPlanet))
            {
                Planets.Add(newPlanet);
                OnPlanetAdded?.Invoke(newPlanet);
                OnPlanetsCountChanged?.Invoke(Planets.Count);
            }
        }

        public void UpgradePlanetsLimit(int amount)
        {
            _maxPlanets += amount;
            OnPlanetsLimitUpgraded?.Invoke(_maxPlanets);
        }

        private void InitializeHeros(List<HeroDataContainer> heroDatas, RankProgressionConfig config)
        {
            Heros.Clear();

            foreach (HeroDataContainer data in heroDatas)
            {
                Heros.Add(new(data, config));
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

        private void InitializeAvailableTypesOfMissions(List<MissionDataContainer> missionDatas)
        {
            Missions.Clear();

            foreach (MissionDataContainer data in missionDatas)
            {
                Missions.Add(data.MissionType);
            }
        }
    }
}