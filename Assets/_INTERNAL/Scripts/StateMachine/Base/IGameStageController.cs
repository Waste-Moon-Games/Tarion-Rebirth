namespace StateMachine.Base
{
    public interface IGameStageController
    {
        void Start();
        void Update();
        void SetStage(IStage newStage);
    }
}