using Core.Common.Abstractions.SceneParams;
using Core.Consts;

namespace Core.ConcreteSceneParams.MainMenu
{
    public class MainMenuEnterParams : SceneEnterParams
    {
        public MainMenuEnterParams() : base(SceneNames.MAIN_MENU)
        {
        }
    }
}