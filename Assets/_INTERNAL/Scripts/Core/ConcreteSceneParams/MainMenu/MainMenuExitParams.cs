using Core.Common.Abstractions.SceneParams;

namespace Core.ConcreteSceneParams.MainMenu
{
    public class MainMenuExitParams
    {
        public SceneEnterParams TartgetSceneEnterParams { get; }

        public MainMenuExitParams(SceneEnterParams tartgetSceneParams)
        {
            TartgetSceneEnterParams = tartgetSceneParams;
        }
    }
}