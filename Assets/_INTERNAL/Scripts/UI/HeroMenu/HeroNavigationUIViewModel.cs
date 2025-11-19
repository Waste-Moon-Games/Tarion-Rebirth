using Core.Common.MVVM;
using R3;

namespace UI.HeroMenu
{
    public enum HeroMenuActions
    {
        Exit, Barracks, Recruit
    }

    public class HeroNavigationUIViewModel : IViewModel
    {
        private HeroInfoModel _model;

        private readonly Subject<HeroMenuActions> _actions = new();

        public Observable<HeroMenuActions> HeroMenuSignals => _actions.AsObservable();

        public void BindModel(IModel model)
        {
            _model = model as HeroInfoModel;
        }

        public void ExitButtonAction()
        {
            _actions.OnNext(HeroMenuActions.Exit);
        }

        public void BarracksButtonAction()
        {
            _model.ToggleBarracks();
            //_actions.OnNext(HeroMenuActions.Barracks);
        }

        public void RecruitButtonAction()
        {
            _model.ToggleRecruit();
            //_actions.OnNext(HeroMenuActions.Recruit);
        }
    }
}
