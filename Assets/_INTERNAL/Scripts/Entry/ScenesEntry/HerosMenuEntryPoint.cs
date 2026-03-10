using Core.DI;
using Core.GameStates;
using Entry.ScenesEntry.ResourceLoaders;
using GameEntity.DataInstance.Main;
using R3;
using SO.Configs;
using UI.HeroMenu.Models;
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
            _resourceLoader
                .LoadBarrackViews(out HeroBarracksView barracksView, out AvailableHeroListView aView, out SelectedHeroInfoView sBInfoVIew);
            _resourceLoader.LoadRecruitViews(out HeroRecruitView recruitView, out AvailableHerosToRecruitListView arView, out SelectedHeroInfoView sRInfoView);

            CreateScene(sceneContainer, imperiumHUD, mainView, barracksView, out ImperiumInstancesHolder instancesHolder, out ImperiumState imperiumState);

            CreateModels(instancesHolder, imperiumState, out HeroMenuModel heroInfoModel);

            CreateMainViewModels(out HeroNavigationUIViewModel navigationViewModel);
            CreateBarracksViewModels(heroInfoModel, aView,
                out HeroBarracksViewModel bViewModel, out AvailableHerosViewModel aViewModel);
            CreateRecruitViewModels(heroInfoModel, arView.transform,
                out AvailableHerosToRecruitViewModel arViewModel,
                out RecruitHerosViewModel rViewModel);

            CreateRecruitViews(recruitView, rViewModel,
                arView, arViewModel, sRInfoView);
            CreateBarracksViews(barracksView, bViewModel, sBInfoVIew, aView, aViewModel);

            mainView.AttachView(recruitView.gameObject);
            mainView.AttachView(barracksView.gameObject);

            barracksView.AttachView(aView.gameObject);
            barracksView.AttachView(sBInfoVIew.gameObject);

            recruitView.AttachUI(arView.gameObject);
            recruitView.AttachUI(sRInfoView.gameObject);

            navigationViewModel.BindModel(heroInfoModel);

            navigationView.BindViewModel(navigationViewModel);

            return navigationViewModel.HeroMenuSignals.Where(action => action == HeroMenuActions.Exit);
        }

        private void CreateScene(DIContainer sceneContainer,
            in ImperiumHUDView imperiumHUD,
            in HerosMenuView mainView,
            in HeroBarracksView bView,
            out ImperiumInstancesHolder instancesHolder,
            out ImperiumState imperiumState)
        {
            var imperiumModel = sceneContainer.Resolve<GameState>().ImperiumState;
            var imperiumViewModel = new ImperiumViewModel();

            imperiumHUD.BindViewModel(imperiumViewModel);
            _imperiumViewModel = imperiumViewModel;
            _imperiumViewModel.BindModel(imperiumModel);

            instancesHolder = imperiumModel.InstanceHolder;
            imperiumState = sceneContainer.Resolve<GameState>().ImperiumState;
        }

        private void CreateModels(in ImperiumInstancesHolder instancesHolder, in ImperiumState imperiumState, out HeroMenuModel heroInfoModel)
        {
            heroInfoModel = new(instancesHolder, imperiumState);
        }

        private void CreateMainViewModels(out HeroNavigationUIViewModel navigationViewModel)
        {
            navigationViewModel = new();
        }

        private void CreateRecruitViewModels(in HeroMenuModel model, in Transform arContainer,
            out AvailableHerosToRecruitViewModel arViewModel,
            out RecruitHerosViewModel rViewModel)
        {
            rViewModel = new();
            arViewModel = new(arContainer);

            rViewModel.BindModel(model.RecruitHerosModel);
            arViewModel.BindModel(model.RecruitHerosModel);
        }

        private void CreateBarracksViewModels(in HeroMenuModel model,
            in AvailableHeroListView aView,
            out HeroBarracksViewModel bViewModel,
            out AvailableHerosViewModel aViewModel)
        {
            aViewModel = new();
            bViewModel = new();

            aViewModel.BindModel(model.HeroBarracksModel);
            bViewModel.BindModel(model.HeroBarracksModel);
        }

        private void CreateRecruitViews(
            in HeroRecruitView rView, in RecruitHerosViewModel rViewModel,
            in AvailableHerosToRecruitListView arView, in AvailableHerosToRecruitViewModel arViewModel,
            in SelectedHeroInfoView sRInfoView)
        {
            rView.BindViewModel(rViewModel);
            arView.BindViewModel(arViewModel);
            sRInfoView.BindViewModel(rViewModel);
        }

        private void CreateBarracksViews(
            in HeroBarracksView bView,
            in HeroBarracksViewModel bViewModel,
            in SelectedHeroInfoView sBInfoView,
            in AvailableHeroListView aView,
            in AvailableHerosViewModel aViewModel)
        {
            aView.BindViewModel(aViewModel);
            bView.BindViewModel(bViewModel);
            sBInfoView.BindViewModel(bViewModel);
        }
    }
}