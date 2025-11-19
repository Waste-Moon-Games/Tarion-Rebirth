using Core.Common.Instances;
using Core.Common.MVVM;
using GameEntity.DataInstance;
using R3;
using UI.PlanetsMap.Models;
using UnityEngine;

namespace UI.PlanetsMap.ViewModels
{
    public class SelectedPlanetViewModel : IViewModel
    {
        private GalaxyMapModel _galaxyMapModel;
        private readonly CompositeDisposable _disposables = new();

        private readonly Subject<PlanetInstance> _addedPlanetSignal = new();
        private readonly Subject<PlanetInstance> _selectPlanetSignal = new();

        public Observable<PlanetInstance> OnPlanetAdded => _addedPlanetSignal.AsObservable();
        public Observable<PlanetInstance> OnPlanetSelected => _selectPlanetSignal.AsObservable();

        public void Init()
        {
            _galaxyMapModel.InstanceAdded.Subscribe(HandleAddedPlanet).AddTo(_disposables);
            _galaxyMapModel.PlanetSelected.Subscribe(HandleSelectedPlanet).AddTo(_disposables);
        }

        public void BindModel(IModel model)
        {
            _galaxyMapModel = model as GalaxyMapModel;
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }

        public void AddPlanetToTargetList(PlanetInstance selectedPlanet)
        {
            _galaxyMapModel.RemoveInstance(selectedPlanet);
            _galaxyMapModel.AddPlanetToTargetList(selectedPlanet);
        }

        private void HandleSelectedPlanet(PlanetInstance selectedPlanet)
        {
            _selectPlanetSignal.OnNext(selectedPlanet);
        }

        private void HandleAddedPlanet(IInstance planet)
        {
            _addedPlanetSignal.OnNext(planet as PlanetInstance);
        }
    }
}