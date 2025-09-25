using GameEntity.DataInstance;
using GameEntity.DataInstance.Main;
using Scripts.GameEntity.DataInstance;
using StateMachine.Base;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Contex.Debug
{
    public static class DebugContex
    {
        [field: SerializeField] public static HeroInstance SelectedHero { get; private set; }
        [field: SerializeField] public static PlanetInstance SelectedPlanet { get; private set; }
        [field: SerializeField] public static List<IGameStageController> StageControllers { get; private set; }
        [field: SerializeField] public static ImperiumInstancesHolder InstanceHolder { get; private set; }

        public static void SetHero(HeroInstance instance) => SelectedHero = instance;

        public static void SetPlanet(PlanetInstance instance) => SelectedPlanet = instance;

        public static void SetController(IGameStageController controller) => StageControllers.Add(controller);

        public static void SetInstanceHolder(ImperiumInstancesHolder instance) => InstanceHolder = instance;
    }
}