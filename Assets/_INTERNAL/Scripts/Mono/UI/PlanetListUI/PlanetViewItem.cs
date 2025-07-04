using GameEntity.DataInstance;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Mono.UI.PlanetListUI
{
    public class PlanetViewItem : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _planetNameText;

        private Button _selectButton;

        private PlanetInstance _planetInstance;

        public event Action<PlanetInstance> OnPlanetSelected;

        private void OnDisable()
        {
            _selectButton.onClick.RemoveListener(() => OnPlanetSelected?.Invoke(_planetInstance));
        }

        public void Setup(PlanetInstance instanceHolder)
        {
            _planetInstance = instanceHolder;
            _planetNameText.text = _planetInstance.RuntimeData.PlanetName;

            _selectButton = GetComponent<Button>();
            _selectButton.onClick.AddListener(() => OnPlanetSelected?.Invoke(_planetInstance));
        }
    }
}