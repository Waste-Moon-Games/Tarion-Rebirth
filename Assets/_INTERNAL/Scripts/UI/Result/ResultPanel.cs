using System;
using UI.Base;
using UnityEngine;

namespace UI.Result
{
    public class ResultPanel : SimpleUIItem
    {
        [field: SerializeField] public ResultUI ResultUI { get; private set; }

        private Action _onResultAccepted;

        public event Action OnResultAccepted;

        private void OnEnable()
        {
            _onResultAccepted = () => OnResultAccepted?.Invoke();
            ResultUI.OnResultAccepted += _onResultAccepted;
        }

        private void OnDisable()
        {
            ResultUI.OnResultAccepted -= _onResultAccepted;
        }
    }
}