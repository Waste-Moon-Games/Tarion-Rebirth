using Core.DI;
using R3;
using UI.MainMenu;
using UI.PlanetsMap;
using UnityEngine;

namespace Entry.Mono.ScenesEntry
{
    public class MapMenuEntryPoint : MonoBehaviour
    {
        [SerializeField] private GalaxyMapView _viewPrefab;

        private DIContainer _sceneContainer;
        private GalaxyMapView _view;

        public Observable<Unit> Run(DIContainer rootContainer)
        {
            _view = _view == null ? Instantiate(_viewPrefab) : _view;
            _sceneContainer ??= new(rootContainer);
            var exitSignal = new Subject<Unit>();

            _view.Bind(_sceneContainer, exitSignal);

            return exitSignal;
        }
    }
}