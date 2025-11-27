using Core.DI;
using Core.GameStates;
using Entry.ScenesEntry.ResourceLoaders;
using GameEntity.DataInstance.Main;
using R3;
using SO.Configs;
using UI.HeroMenu.Models;
using UI.HeroMenu.ViewModel;
using UI.HeroMenu.ViewModels;
using UI.HeroMenu.Views;
using UI.HeroUI;
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
                .LoadMainViews
                (out HerosMenuView mainView, out ImperiumHUDView imperiumHUD, out UINavigationRootView navigationView);
            _resourceLoader.LoadBarrackViews(out HeroBarracksView barracksView, out AvailableHeroListView aView);
            _resourceLoader.LoadRecruitViews(out HeroRecruitView recruitView);
            _resourceLoader.LoadConfigs(out AvailableHerosPoolConfig herosPoolConfig);

            CreateScene(sceneContainer, imperiumHUD, mainView, barracksView, out ImperiumInstancesHolder instancesHolder);

            CreateModels(instancesHolder, out HeroInfoModel heroInfoModel);
            CreateViewModels(heroInfoModel, herosPoolConfig,
                out RecruitHerosViewModel rViewModel,
                out HeroBarracksViewModel bViewModel,
                out HeroNavigationUIViewModel navigationViewModel,
                out AvailableHerosViewModel aViewModel);
            CreateViews(recruitView, barracksView, aView, bViewModel, rViewModel, aViewModel);

            mainView.AttachView(recruitView.gameObject);
            mainView.AttachView(barracksView.gameObject);

            barracksView.AttachView(aView.gameObject);

            navigationViewModel.BindModel(heroInfoModel);

            navigationView.BindViewModel(navigationViewModel);

            return navigationViewModel.HeroMenuSignals.Where(action => action == HeroMenuActions.Exit);
        }

        private void CreateScene(DIContainer sceneContainer,
            in ImperiumHUDView imperiumHUD,
            in HerosMenuView mainView,
            in HeroBarracksView bView,
            out ImperiumInstancesHolder instancesHolder)
        {
            var imperiumModel = sceneContainer.Resolve<GameState>().ImperiumState;
            var imperiumViewModel = new ImperiumViewModel(imperiumModel);

            imperiumHUD.Bind(imperiumViewModel);
            _imperiumViewModel = imperiumViewModel;
            mainView.Bind(imperiumModel.InstanceHolder, bView.OwnedHeroInfoHolderView);

            instancesHolder = imperiumModel.InstanceHolder;
        }

        private void CreateModels(in ImperiumInstancesHolder instancesHolder, out HeroInfoModel heroInfoModel)
        {
            heroInfoModel = new(instancesHolder);
        }

        private void CreateViewModels(in HeroInfoModel model,
            in AvailableHerosPoolConfig aConfig,
            out RecruitHerosViewModel rViewModel,
            out HeroBarracksViewModel bViewModel,
            out HeroNavigationUIViewModel navigationViewModel,
            out AvailableHerosViewModel aViewModel)
        {
            rViewModel = new();
            bViewModel = new();
            navigationViewModel = new();
            aViewModel = new(aConfig);

            rViewModel.BindModel(model.RecruitHerosModel);
            bViewModel.BindModel(model.HeroBarracksModel);
            aViewModel.BindModel(model.HeroBarracksModel);
        }

        private void CreateViews(
            in HeroRecruitView rView,
            in HeroBarracksView bView,
            in AvailableHeroListView aView,
            in HeroBarracksViewModel bViewModel,
            in RecruitHerosViewModel rViewModel,
            in AvailableHerosViewModel aViewModel)
        {
            rView.BindViewModel(rViewModel);
            bView.BindViewModel(bViewModel);
            aView.BindViewModel(aViewModel);
        }
    }
}