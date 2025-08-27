using GameEntity.DataInstance;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.PlanetsMap
{
    public class PlanetMapView : MonoBehaviour
    {
        [field: SerializeField] public RectTransform CurrentPosition { get; private set; }
        [SerializeField] private Image _planetSprite;

        private Button _selectButton;
        private PlanetInstance _planet;

        private UnityAction _clickHandler;

        public PlanetInstance Planet => _planet;

        public event Action<PlanetInstance> OnPlanetSelected;

        private void OnDestroy()
        {
            _selectButton.onClick.RemoveListener(_clickHandler);
        }

        private void Awake()
        {
            CurrentPosition = GetComponent<RectTransform>();
            _clickHandler = () => OnPlanetSelected?.Invoke(_planet);

            if(_selectButton == null)
            {
                _selectButton = GetComponent<Button>();
                _selectButton.onClick.AddListener(_clickHandler);
            }
        }

        public void SetupView(PlanetInstance planet)
        {
            _planet = planet;
        }

        public void SetupPosition(Vector2 position)
        {
            CurrentPosition.anchoredPosition = position;
        }
    }
}