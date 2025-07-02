using StateMachine.Base;
using UnityEngine;

namespace StateMachine.Stages
{
    public class PlanetSelectionStage : IStage
    {
        private readonly IGameStageController _controller;

        public PlanetSelectionStage(IGameStageController controller)
        {
            _controller = controller;
        }

        public void Enter()
        {
            Debug.Log("Planet Selection Stage: Enter");
            // TODO: здесь будет выбор планеты
            // Пока переключим на следующую стадию для прототипа
            _controller.SetStage(new HeroSelectionStage(_controller));
        }

        public void Exit()
        {
            Debug.Log("Planet Selectio Stage: Exit");
        }

        public void Tick()
        {
            //Обычно пусто (ждём ввода игрока)
        }
    }
}
