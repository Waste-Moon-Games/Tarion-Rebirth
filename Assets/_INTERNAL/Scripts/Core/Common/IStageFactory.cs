using StateMachine.Base;
using System;

namespace Core.Factories
{
    public interface IStageFactory
    {
        event Action OnMissionExecutionStageCreated;
        IStage CreatePlanetSelectionStage(IGameStageController controller);
        IStage CreateHeroSelectionStage(IGameStageController controller);
        IStage CreateMissionTypeSelectionStage(IGameStageController controller);
        IStage CreateMissionPreparationStage(IGameStageController controller);
        IStage CreateMissionExecutionStage(IGameStageController controller);
        IStage CreateMissionResultStage(IGameStageController controller);
    }
}