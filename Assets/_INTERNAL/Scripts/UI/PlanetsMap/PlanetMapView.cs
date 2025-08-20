using GameEntity.DataInstance;
using UnityEngine;
using UnityEngine.UI;

namespace UI.PlanetsMap
{
    public class PlanetMapView : MonoBehaviour
    {
        [field: SerializeField] public RectTransform CurrentPosition { get; private set; }
        [SerializeField] private Image _planetSprite;

        private void Awake()
        {
            CurrentPosition = GetComponent<RectTransform>();
        }

        public void SetupView(PlanetInstance planet)
        {
        }

        public void SetupPosition(Vector2 position)
        {
            CurrentPosition.anchoredPosition = position;
        }
    }
}