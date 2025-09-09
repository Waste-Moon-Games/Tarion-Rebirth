using Core.Common;

namespace StateMachine.Base
{
    public interface IStage
    {
        void Enter();
        void RefreshDeps(IDependence dependence);
        void Tick();
        void Exit();
    }
}