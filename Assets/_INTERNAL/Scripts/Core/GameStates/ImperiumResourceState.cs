using Core.EntityDatas.Resource;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.GameStates
{
    public class ImperiumResourceState
    {
        private readonly Dictionary<ResourceType, int> _resources = new();

        public event Action<ResourceType, int> OnResourceChanged;

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

            OnResourceChanged?.Invoke(id, Get(id));
        }

        public int Get(ResourceType type) => _resources.TryGetValue(type, out int value) ? value : 0;
    }
}