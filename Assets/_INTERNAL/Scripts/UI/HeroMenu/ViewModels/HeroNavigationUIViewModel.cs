using Core.Common.MVVM;
using R3;
using UI.HeroMenu.Models;

namespace UI.HeroMenu.ViewModels
{
    public enum HeroMenuActions
    {
        Exit, Barracks, Recruit
    }

    public class HeroNavigationUIViewModel : IViewModel
    {
        private HeroMenuModel _model;

        private readonly Subject<HeroMenuActions> _actions = new();

        public Observable<HeroMenuActions> HeroMenuSignals => _actions.AsObservable();

        public void BindModel(IModel model)
        {
            _model = model as HeroMenuModel;
            _model.ToggleBarracks();
        }

        public void Dispose() { }

        public void ExitButtonAction()
        {
            _actions.OnNext(HeroMenuActions.Exit);
        }

        public void BarracksButtonAction()
        {
            _model.ToggleBarracks();
        }

        public void RecruitButtonAction()
        {
            _model.ToggleRecruit();
        }
    }
}
