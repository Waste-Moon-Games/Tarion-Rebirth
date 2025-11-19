using Core.Common.MVVM;
using GameEntity.DataInstance;
using R3;
using System.Collections.Generic;
using UI.PlanetsMap.Models;

namespace UI.PlanetsMap.ViewModels
{
    public class GalaxyMapViewModel : IViewModel
    {
        private GalaxyMapModel _model;
        private readonly CompositeDisposable _disposables = new();

        private readonly Subject<List<PlanetMapView>> _mapRefreshed = new();

        public Observable<List<PlanetMapView>> MapRefreshed => _mapRefreshed.AsObservable();

        public void BindModel(IModel model)
        {
            _model = model as GalaxyMapModel;
        }

        public void Init()
        {
            Subscribe();
        }

        private void Subscribe()
        {
            _model.MapRefreshed.Subscribe(HandleRefreshedMap).AddTo(_disposables);
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }

        public void HandleSelectedPlanet(PlanetInstance selectedPlanet)
        {
            _model.SelectPlanet(selectedPlanet);
        }

        private void HandleRefreshedMap(List<PlanetMapView> newPlanets)
        {
            _mapRefreshed.OnNext(newPlanets);
        }
    }
}