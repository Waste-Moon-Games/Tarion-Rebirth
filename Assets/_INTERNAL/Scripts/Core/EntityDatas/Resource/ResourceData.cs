using UnityEngine;

namespace Core.EntityDatas.Resource
{
    [System.Serializable]
    public class ResourceData
    {
        public string Name;

        public string Id;

        [TextArea]
        public string Description;

        public int Amount;

        public ResourceType Type;
    }
}