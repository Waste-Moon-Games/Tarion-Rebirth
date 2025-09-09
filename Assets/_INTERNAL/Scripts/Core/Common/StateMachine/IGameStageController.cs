using Core.Factories;

namespace StateMachine.Base
{
    public interface IGameStageController
    {
        IStageFactory StageFactory { get; }
        void StartCycle();
        void Update();
        void SetStage(IStage newStage);
        void EndCycle();
        void ForceEnd();
    }
}