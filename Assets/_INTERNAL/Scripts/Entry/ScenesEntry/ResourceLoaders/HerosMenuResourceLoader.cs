using SO.Configs;
using UI.HeroMenu.Views;
using UI.HeroUI;
using UI.MainMenu;
using UnityEngine;

namespace Entry.ScenesEntry.ResourceLoaders
{
    public sealed class HerosMenuResourceLoader
    {
        public void LoadMainViews(out HerosMenuView mainView, out ImperiumHUDView imperiumHUDView, out UINavigationRootView navigationView)
        {
            var mainViewPrefab = Resources.Load<HerosMenuView>("UI/Views/Heros/HerosInfoUI");
            var imperiumHUDViewPrefab = Resources.Load<ImperiumHUDView>("UI/Views/Common/ImperiumHUD");
            var navigationViewPrefab = Resources.Load<UINavigationRootView>("UI/Root/UINavigationRootView");

            mainView = Object.Instantiate(mainViewPrefab);
            imperiumHUDView = Object.Instantiate(imperiumHUDViewPrefab);
            navigationView = Object.Instantiate(navigationViewPrefab);
        }

        public void LoadBarrackViews(out HeroBarracksView heroBarracksView, out AvailableHeroListView aView)
        {
            var hbPrefab = Resources.Load<HeroBarracksView>("UI/Views/Heros/Barracks/BarracksView");
            var aPrefab = Resources.Load<AvailableHeroListView>("UI/Views/Heros/Barracks/AvailableHerosListView");

            heroBarracksView = Object.Instantiate(hbPrefab);
            aView = Object.Instantiate(aPrefab);
        }

        public void LoadRecruitViews(out HeroRecruitView heroRecruitView)
        {
            var hrPrefab = Resources.Load<HeroRecruitView>("UI/Views/Heros/Recruit/RecruitView");

            heroRecruitView = Object.Instantiate(hrPrefab);
        }

        public void LoadConfigs(out AvailableHerosPoolConfig herosPoolConfig)
        {
            herosPoolConfig = Resources.Load<AvailableHerosPoolConfig>("Configs/HeroMenu/AvailableHerosPoolConfig");
        }
    }
}