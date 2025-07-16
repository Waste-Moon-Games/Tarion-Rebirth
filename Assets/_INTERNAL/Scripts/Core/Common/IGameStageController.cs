using Core.Factories;

namespace StateMachine.Base
{
    public interface IGameStageController
    {
        IStageFactory StageFactory { get; }
        void Start();
        void Update();
        void SetStage(IStage newStage);
        void ExitStage();
    }
}