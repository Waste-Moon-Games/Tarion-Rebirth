using GameEntity.DataInstance;
using R3;
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
        private readonly Subject<PlanetInstance> _planetSelected = new();

        public PlanetInstance Planet => _planet;

        public Observable<PlanetInstance> OnPlanetSelected => _planetSelected.AsObservable();

        private void OnDestroy()
        {
            _selectButton.onClick.RemoveListener(PlanetSelectButton);
        }

        private void Awake()
        {
            CurrentPosition = GetComponent<RectTransform>();

            if(_selectButton == null)
            {
                _selectButton = GetComponent<Button>();
                _selectButton.onClick.AddListener(PlanetSelectButton);
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

        private void PlanetSelectButton()
        {
            _planetSelected.OnNext(_planet);
        }
    }
}