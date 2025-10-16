using Core.Common.Abstractions.RecruitSystem;
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
    public class ImperiumInstancesHolder : IInstanceHolderWriteService
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

        public event Action<HeroInstance> OnHeroRejected;
        public event Action<int> OnHerosCountChanged;
        public event Action<HeroInstance> OnHerosListUpdated;
        public event Action<int> OnHerosLimitUpgraded;

        public ImperiumInstancesHolder(ImperiumConfig limitsConfig)
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

        public void AddNewInstance(IInstance hero)
        {
            var newHero = hero as HeroInstance;

            if (Heros.Count >= _maxHeros)
            {
                OnHeroRejected?.Invoke(newHero);
                return;
            }

            if (!Heros.Contains(newHero))
            {
                Heros.Add(newHero);
                OnHerosListUpdated?.Invoke(newHero);
                OnHerosCountChanged?.Invoke(Heros.Count);
            }
        }

        public void UpgradePlanetsLimit(int amount)
        {
            _maxPlanets += amount;
            OnPlanetsLimitUpgraded?.Invoke(_maxPlanets);
        }

        public void UpgradeHerosLimit(int amount)
        {
            _maxHeros += amount;
            OnHerosLimitUpgraded?.Invoke(_maxHeros);
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