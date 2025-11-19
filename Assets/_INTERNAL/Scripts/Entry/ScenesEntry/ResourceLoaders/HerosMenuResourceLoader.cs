using UI;
using UI.HeroMenu;
using UI.MainMenu;
using UnityEngine;

namespace Entry.ScenesEntry.ResourceLoaders
{
    public sealed class HerosMenuResourceLoader
    {
        public void LoadViews(out HerosMenuView mainView, out ImperiumHUDView imperiumHUDView, out UINavigationRootView navigationView)
        {
            var mainViewPrefab = Resources.Load<HerosMenuView>("UI/Views/HerosInfoUI");
            var imperiumHUDViewPrefab = Resources.Load<ImperiumHUDView>("UI/Common/ImperiumHUD");
            var navigationViewPrefab = Resources.Load<UINavigationRootView>("UI/Root/UINavigationRootView");

            mainView = Object.Instantiate(mainViewPrefab);
            imperiumHUDView = Object.Instantiate(imperiumHUDViewPrefab);
            navigationView = Object.Instantiate(navigationViewPrefab);
        }
    }
}