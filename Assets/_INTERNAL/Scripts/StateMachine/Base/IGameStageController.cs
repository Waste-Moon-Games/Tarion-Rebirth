using GameEntity.DataInstance.Main;
using Mono.UI.PlanetListUI;

namespace StateMachine.Base
{
    public interface IGameStageController
    {
        void Start();
        void Update();
        void SetStage(IStage newStage);
    }
}