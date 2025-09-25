using GameEntity.DataInstance;
using GameEntity.DataInstance.Main;
using System.Collections;
using UnityEngine;
using Utils.ModCoroutines;

namespace Core.GameStates
{
    public class ImperiumResourceService
    {
        private readonly ImperiumInstancesHolder _instanceHolder;
        private readonly ImperiumResourceState _resources;

        private readonly float _extractionTime;
        private readonly float _capturedResourcesMuiltiplier;
        private Coroutine _resourceRoutine;

        public ImperiumResourceService(ImperiumInstancesHolder instanceHolder, ImperiumResourceState resources, float extractionTime, float capturedResourcesMuiltiplier)
        {
            _instanceHolder = instanceHolder;
            _resources = resources;
            _extractionTime = extractionTime;
            _capturedResourcesMuiltiplier = capturedResourcesMuiltiplier;
        }

        public void StartExtraction()
        {
            if (_resourceRoutine != null)
                Coroutines.StopRoutine(_resourceRoutine);

            _resourceRoutine = Coroutines.StartRoutine(Extract());
        }

        public void GetCapturedResources(PlanetInstance capturedPlanet)
        {
            foreach (var extractor in capturedPlanet.GetExtractors())
            {
                _resources.Add(extractor.ResourceData.Type, (int)Mathf.Pow(extractor.Extract(), _capturedResourcesMuiltiplier));
            }
        }

        public IEnumerator Extract()
        {
            while(true)
            {
                if (_instanceHolder.Planets.Count > 0)
                {
                    foreach (var planet in _instanceHolder.Planets)
                    {
                        foreach (var extractor in planet.GetExtractors())
                        {
                            _resources.Add(extractor.ResourceData.Type, extractor.Extract());
                        }
                    }
                }
                yield return new WaitForSeconds(_extractionTime);
            }
        }

        public void StopExtaction()
        {
            if(_resourceRoutine != null)
            {
                Coroutines.StopRoutine(_resourceRoutine);
                _resourceRoutine = null;
            }
        }
    }
}