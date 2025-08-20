using GameEntity.DataInstance;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace UI.PlanetsMap
{
    public class GalaxyMapSpawner : MonoBehaviour
    {
        [SerializeField] private float _minSpawnDistance;
        [SerializeField] private RectTransform _mapArea;
        [SerializeField] private PlanetMapView _planetMapViewPrefab;

        private List<PlanetMapView> _spawnedPlanets = new();
        private List<Vector2> _usedPositions = new();

        public void SpawnPlanets(List<PlanetInstance> planets)
        {
            ClearMap();

            foreach (PlanetInstance planet in planets)
            {
                PlanetMapView view = Instantiate(_planetMapViewPrefab, _mapArea);

                Vector2 pos = GetUniquePosition(_usedPositions, view.CurrentPosition.sizeDelta);

                view.SetupView(planet);
                view.SetupPosition(pos);

                _spawnedPlanets.Add(view);
            }
        }

        private Vector2 GetRandomPositionInside(RectTransform area, Vector2 planetSize)
        {
            float x = Random.Range(
                -area.rect.width / 2f + planetSize.x / 2f,
                area.rect.width / 2f - planetSize.x / 2f
                );
            float y = Random.Range(
                -area.rect.height / 2f + planetSize.y / 2f,
                area.rect.height / 2f - planetSize.y / 2f
                );
            return new Vector2(x, y);
        }

        private Vector2 GetUniquePosition(List<Vector2> usedPositions, Vector2 planetSize)
        {
            while (true)
            {
                Vector2 pos = GetRandomPositionInside(_mapArea, planetSize);

                bool occupied = usedPositions.Any(u => Vector2.Distance(pos, u) < _minSpawnDistance);

                if (!occupied)
                {
                    usedPositions.Add(pos);
                    return pos;
                }
            }
        }

        private void ClearMap()
        {
            foreach (var planet in _spawnedPlanets)
            {
                Destroy(planet.gameObject);
            }
            _spawnedPlanets.Clear();
            _usedPositions.Clear();
        }
    }
}