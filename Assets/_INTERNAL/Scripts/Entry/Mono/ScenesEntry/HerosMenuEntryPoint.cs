using Core.DI;
using R3;
using UI;
using UI.HeroDetailInfoUI;
using UnityEngine;

namespace Entry.Mono.ScenesEntry
{
    public class HerosMenuEntryPoint : MonoBehaviour
    {
        [SerializeField] private HerosMenuView _viewPrefab;
        [SerializeField] private UIHerosMenuViewBinder _binder;

        private HerosMenuView _view;

        public Observable<Unit> Run(DIContainer sceneContainer)
        {
            var exitSignal = new Subject<Unit>();
            var container = new DIContainer(sceneContainer);
            _view = _view == null ? Instantiate(_viewPrefab) : _view;
            _binder.Bind(container, exitSignal, _view);

            return exitSignal;
        }
    }
}