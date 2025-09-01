using StateMachine.Base;
using StateMachine.Stages;
using System;

namespace Core.Factories.Stage_Factory
{
    public class StageFactory : IStageFactory
    {
        private readonly StageDependencies _deps;

        public event Action OnMissionExecutionStageCreated;

        public StageFactory(StageDependencies dependencies)
        {
            _deps = dependencies;
        }

        public IStage CreatePlanetSelectionStage(IGameStageController controller) => new PlanetSelectionStage(controller, _deps);

        public IStage CreateHeroSelectionStage(IGameStageController controller) => new HeroSelectionStage(controller, _deps);

        public IStage CreateMissionTypeSelectionStage(IGameStageController controller) => new MissionTypeSelectionStage(controller, _deps);

        public IStage CreateMissionPreparationStage(IGameStageController controller) => new MissionPreparationStage(controller, _deps);

        public IStage CreateMissionExecutionStage(IGameStageController controller)
        {
            OnMissionExecutionStageCreated?.Invoke();
            return new MissionExecutionStage(controller, _deps);
        }

        public IStage CreateMissionResultStage(IGameStageController controller) => new MissionResultStage(controller, _deps);
    }
}