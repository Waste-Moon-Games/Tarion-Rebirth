using Core.Contex.Debug;
using Stages.StageController;
using UnityEngine;

namespace Debugger
{
    public static class DebugActions
    {
        public static void AddExpToCurrentHero(int exp)
        {
            var hero = DebugContex.SelectedHero;

            hero.AddExperience(exp);
            Debug.Log($"[DEBUG] Добавлено {exp} опыта герою {hero.RuntimeData.Name}");
        }

        public static void ForceCompleteMission()
        {
            GameStageController controller = DebugContex.StageController as GameStageController;
            var stage = controller.StageFactory.CreateMissionResultStage(controller);

            controller.SetStage(stage);
            Debug.Log($"[DEBUG] Миссия принудительно завершена");
        }
    }
}