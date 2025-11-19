using Core.DI;
using Core.GameStates;
using Entry.ScenesEntry.ResourceLoaders;
using R3;
using UI;
using UI.HeroMenu;
using UI.MainMenu;
using UnityEngine;

namespace Entry.Mono.ScenesEntry
{
    public class HerosMenuEntryPoint : MonoBehaviour
    {
        private readonly HerosMenuResourceLoader _resourceLoader = new();
        private ImperiumViewModel _imperiumViewModel;

        private void OnDestroy()
        {
            _imperiumViewModel.Dispose();
        }

        public Observable<HeroMenuActions> Run(DIContainer sceneContainer)
        {
            _resourceLoader
                .LoadViews
                (out HerosMenuView mainView, out ImperiumHUDView imperiumHUD, out UINavigationRootView navigationView);

            var imperiumModel = sceneContainer.Resolve<GameState>().ImperiumState;
            var imperiumViewModel = new ImperiumViewModel(imperiumModel);

            var navigationViewModel = new HeroNavigationUIViewModel();
            navigationView.BindViewModel(navigationViewModel);

            imperiumHUD.Bind(imperiumViewModel);
            _imperiumViewModel = imperiumViewModel;
            mainView.Bind(imperiumModel.InstanceHolder);

            return navigationViewModel.HeroMenuSignals.Where(action => action == HeroMenuActions.Exit);
        }
    }
}