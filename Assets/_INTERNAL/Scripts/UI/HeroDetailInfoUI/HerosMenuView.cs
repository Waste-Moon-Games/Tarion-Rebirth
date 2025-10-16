using Core.DI;
using R3;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HerosMenuView : MonoBehaviour
    {
        private Subject<Unit> _exitSignalSubj;
        private DIContainer _sceneContainer;
        private Button _exitButton;

        public void Bind(DIContainer container, Subject<Unit> exitSignalSubj, Button exitButton)
        {
            _sceneContainer = container;
            _exitSignalSubj = exitSignalSubj;

            _exitButton = exitButton;

            _exitButton.OnClickAsObservable()
                .ThrottleFirst(TimeSpan.FromSeconds(0.5f))
                .Subscribe(_ => HandleExitButtonClick())
                .AddTo(this);
        }

        private void HandleExitButtonClick()
        {
            _exitSignalSubj?.OnNext(Unit.Default);
        }
    }
}