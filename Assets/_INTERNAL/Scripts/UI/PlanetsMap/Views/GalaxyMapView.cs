using Core.Common.MVVM;
using GameEntity.DataInstance;
using R3;
using System.Collections.Generic;
using UI.PlanetsMap.ViewModels;
using UnityEngine;

namespace UI.PlanetsMap
{
    public class GalaxyMapView : MonoBehaviour, IView
    {
        [field: SerializeField] public RectTransform SpawnArea { get; private set; }

        [SerializeField] private List<PlanetMapView> _spawnedPlanets = new();

        private readonly CompositeDisposable _disposables = new();

        private GalaxyMapViewModel _viewModel;

        private void OnDestroy()
        {
            _disposables.Dispose();
        }

        public void BindViewModel(IViewModel viewModel)
        {
            _viewModel = viewModel as GalaxyMapViewModel;

            _viewModel.MapRefreshed.Subscribe(HandleRefreshedMap).AddTo(_disposables);
        }

        private void HandleRefreshedMap(List<PlanetMapView> spawnedPlanets)
        {
            _spawnedPlanets.Clear();

            foreach (PlanetMapView view in spawnedPlanets)
            {
                view.OnPlanetSelected.Subscribe(HandleSelectedPlanet).AddTo(_disposables);
                _spawnedPlanets.Add(view);
            }
        }

        private void HandleSelectedPlanet(PlanetInstance selectedPlanet)
        {
            _viewModel.HandleSelectedPlanet(selectedPlanet);
        }
    }
}