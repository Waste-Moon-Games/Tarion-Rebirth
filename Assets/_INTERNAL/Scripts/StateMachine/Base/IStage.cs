namespace StateMachine.Base
{
    public interface IStage
    {
        void Enter();
        void Tick();
        void Exit();
    }
}