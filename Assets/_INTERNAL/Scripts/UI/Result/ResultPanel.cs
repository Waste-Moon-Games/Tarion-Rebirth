using System;
using UI.Base;
using UnityEngine;

namespace UI.Result
{
    public class ResultPanel : SimpleUIItem
    {
        [field: SerializeField] public ResultUI ResultUI { get; private set; }

        public event Action OnResultAccepted;

        private void OnEnable()
        {
            ResultUI.OnResultAccepted += ()=> OnResultAccepted?.Invoke();
        }

        private void OnDisable()
        {
            ResultUI.OnResultAccepted -= ()=> OnResultAccepted?.Invoke();
        }
    }
}