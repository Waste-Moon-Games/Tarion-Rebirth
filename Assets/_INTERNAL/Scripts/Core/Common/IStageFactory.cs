using StateMachine.Base;

namespace Core.Factories
{
    public interface IStageFactory
    {
        IStage CreatePlanetSelectionStage(IGameStageController controller);
        IStage CreateHeroSelectionStage(IGameStageController controller);
        IStage CreateMissionTypeSelectionStage(IGameStageController controller);
        IStage CreateMissionPreparationStage(IGameStageController controller);
        IStage CreateMissionExecutionStage(IGameStageController controller);
        IStage CreateMissionResultStage(IGameStageController controller);
    }
}