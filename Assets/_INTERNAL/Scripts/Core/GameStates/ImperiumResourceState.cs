using Core.EntityDatas.Resource;
using R3;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.GameStates
{
    public class ImperiumResourceState
    {
        private readonly Subject<Dictionary<ResourceType, int>> _requestResourcesCountSignal = new();
        private readonly Dictionary<ResourceType, int> _resources = new();

        public Observable<Dictionary<ResourceType, int>> RequestResources => _requestResourcesCountSignal.AsObservable();

        public ImperiumResourceState()
        {
            _resources = Enum.GetValues(typeof(ResourceType))
                .Cast<ResourceType>().
                ToDictionary(rt => rt, rt => 0);
        }

        public void Add(ResourceType id, int amount)
        {
            if (!_resources.ContainsKey(id))
                _resources[id] = 0;

            _resources[id] += amount;

            _requestResourcesCountSignal.OnNext(_resources);
        }

        public void Spend(ResourceType id, int amount)
        {
            if (!_resources.ContainsKey(id))
                return;

            if (_resources[id] < amount)
                return;

            _resources[id] -= amount;

            _requestResourcesCountSignal.OnNext(_resources);
        }

        public void RequestResourcesState() => _requestResourcesCountSignal.OnNext(_resources);
    }
}