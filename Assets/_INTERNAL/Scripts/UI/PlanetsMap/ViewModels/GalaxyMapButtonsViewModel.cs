using R3;
using UI.PlanetsMap.Models;

namespace UI.PlanetsMap.ViewModels
{
    public enum GalaxyMapActions
    {
        RefreshMap, CloseMap
    }

    public class GalaxyMapButtonsViewModel
    {
        private readonly GalaxyMapModel _model;
        private readonly Subject<GalaxyMapActions> _actions;

        public Observable<GalaxyMapActions> Actions => _actions.AsObservable();

        public GalaxyMapButtonsViewModel(GalaxyMapModel model, Subject<GalaxyMapActions> actions)
        {
            _model = model;
            _actions = actions;
        }

        public void RefreshMap()
        {
            _model.RefreshMap();
            _actions.OnNext(GalaxyMapActions.RefreshMap);
        }

        public void CloseMap()
        {
            _actions.OnNext(GalaxyMapActions.CloseMap);
        }
    }
}