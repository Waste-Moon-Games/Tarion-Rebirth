using GameEntity.DataInstance;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;

namespace UI.PlanetsMap
{
    public class GalaxyMapSpawner : MonoBehaviour
    {
        [SerializeField] private float _minSpawnDistance;
        [SerializeField] private RectTransform _mapArea;
        [SerializeField] private PlanetMapView _planetMapViewPrefab;
        [SerializeField] private bool _autoExpand;

        private readonly List<PlanetMapView> _spawnedPlanets = new();
        private readonly List<Vector2> _usedPositions = new();

        private ObjectPool<PlanetMapView> _planetsPool;

        public void CreatePlanetsPool(int count)
        {
            _planetsPool = new(_planetMapViewPrefab, count, _mapArea)
            {
                AutoExpand = _autoExpand
            };
        }

        public List<PlanetMapView> SpawnPlanets(List<PlanetInstance> planets)
        {
            ClearMap();

            foreach (PlanetInstance planet in planets)
            {
                PlanetMapView view = _planetsPool.GetFreeElement();

                Vector2 pos = GetUniquePosition(_usedPositions, view.CurrentPosition.sizeDelta);

                view.SetupView(planet);
                view.SetupPosition(pos);

                _spawnedPlanets.Add(view);
            }

            return _spawnedPlanets;
        }

        public void RemovePlanet(PlanetInstance planet)
        {
            PlanetMapView view = _spawnedPlanets.FirstOrDefault(p => p.Planet == planet);

            if(view != null)
            {
                _spawnedPlanets.Remove(view);
                _planetsPool.ReturnToPool(view);
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
                _planetsPool.ReturnToPool(planet);
            }
            _spawnedPlanets.Clear();
            _usedPositions.Clear();
        }
    }
}