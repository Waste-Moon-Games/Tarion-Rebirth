using Core.DI;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace UI.PlanetsMap
{
    public class GalaxyMapView : MonoBehaviour
    {
        private Subject<Unit> _exitSignalSubj;
        private DIContainer _sceneContainer;

        [SerializeField] private Button _exitButton;

        private void Start()
        {
            if (_exitButton == null)
                return;

            _exitButton.onClick.AddListener(HandleExitButtonClick);
        }

        private void OnDestroy()
        {
            _exitButton.onClick.RemoveListener(HandleExitButtonClick);
        }

        public void Bind(DIContainer entryPointContainer, Subject<Unit> exitSignal)
        {
            _sceneContainer = new(entryPointContainer);
            _exitSignalSubj = exitSignal;
        }

        private void HandleExitButtonClick()
        {
            _exitSignalSubj?.OnNext(Unit.Default);
        }
    }
}