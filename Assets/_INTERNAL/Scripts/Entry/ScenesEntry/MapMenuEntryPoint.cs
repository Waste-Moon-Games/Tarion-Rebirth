using Core.DI;
using R3;
using SO.Containers.Configs;
using UI.Base;
using UI.PlanetsMap;
using UI.PlanetsMap.Models;
using UI.PlanetsMap.ViewModels;
using UnityEngine;

namespace Entry.Mono.ScenesEntry
{
    public class MapMenuEntryPoint : MonoBehaviour
    {
        private DIContainer _sceneContainer;

        private GalaxyMapButtonsViewModel _buttonsViewModel;
        private GalaxyMapViewModel _mapViewModel;
        private SelectedPlanetViewModel _selectedPlanetViewModel;

        private GalaxyMapModel _galaxyMapModel;

        private void OnDestroy()
        {
            _sceneContainer.Dispose();
            _mapViewModel.Dispose();
            _selectedPlanetViewModel.Dispose();
        }

        public Observable<GalaxyMapActions> Run(DIContainer rootContainer)
        {
            _sceneContainer ??= new(rootContainer);

            var actions = new Subject<GalaxyMapActions>();

            LoadResources(
                out GalaxyMapSpawner spawner,
                out PlanetsGenerationConfig config,
                out UIGalaxyMapRootView rootUIContainer);

            CreateModels(spawner, config);
            CreateViewModels(actions);
            LoadViews(
                out GalaxyMapView galaxyMapView,
                out GalaxyMapButtonsView galaxyMapButtonsView,
                out SelectedPlanetView selectedPlanetView,
                rootUIContainer);

            _galaxyMapModel.Init(galaxyMapView.SpawnArea);

            _mapViewModel.BindModel(_galaxyMapModel);
            _selectedPlanetViewModel.BindModel(_galaxyMapModel);

            _mapViewModel.Init();
            _selectedPlanetViewModel.Init();

            galaxyMapView.BindViewModel(_mapViewModel);
            selectedPlanetView.BindViewModel(_selectedPlanetViewModel);
            galaxyMapButtonsView.BindViewModel(_buttonsViewModel);

            return _buttonsViewModel.Actions.Where(action => action == GalaxyMapActions.CloseMap);
        }

        private void CreateViewModels(Subject<GalaxyMapActions> actions)
        {
            _buttonsViewModel ??= new(_galaxyMapModel, actions);
            _mapViewModel ??= new();
            _selectedPlanetViewModel ??= new();
        }

        private void CreateModels(GalaxyMapSpawner spawner, PlanetsGenerationConfig config)
        {
            _galaxyMapModel ??= new(spawner, config, _sceneContainer);
        }

        private void LoadResources(
            out GalaxyMapSpawner spawner,
            out PlanetsGenerationConfig config,
            out UIGalaxyMapRootView rootUIContainer)
        {
            var spawnerPrefab = Resources.Load<GalaxyMapSpawner>("Services/MapMenu/GalaxyMapSpawnerService");
            spawner = Instantiate(spawnerPrefab);

            config = Resources.Load<PlanetsGenerationConfig>("Configs/MapMenu/PlanetsGenerationConfig");

            var rootUIContainerPrefab = Resources.Load<UIGalaxyMapRootView>("UI/Root/GalaxyMapRootView");
            rootUIContainer = Instantiate(rootUIContainerPrefab);
        }

        private void LoadViews(
            out GalaxyMapView galaxyMapView,
            out GalaxyMapButtonsView galaxyMapButtonsView,
            out SelectedPlanetView selectedPlanetView,
            UIGalaxyMapRootView rootView)
        {
            var galaxyMapViewPrefab = Resources.Load<GalaxyMapView>("UI/Views/Map/GalaxyMapView");
            galaxyMapView = Instantiate(galaxyMapViewPrefab);
            rootView.AttachSceneUI(galaxyMapView.gameObject);

            var galaxyMapButtonsViewPrefab = Resources.Load<GalaxyMapButtonsView>("UI/Views/Map/GalaxyMapButtonsView");
            galaxyMapButtonsView = Instantiate(galaxyMapButtonsViewPrefab);
            rootView.AttachSceneUI(galaxyMapButtonsView.gameObject);

            var selectedPlanetViewPrefab = Resources.Load<SelectedPlanetView>("UI/Views/Map/SelectedPlanetView");
            selectedPlanetView = Instantiate(selectedPlanetViewPrefab, galaxyMapView.transform, false);
        }
    }
}