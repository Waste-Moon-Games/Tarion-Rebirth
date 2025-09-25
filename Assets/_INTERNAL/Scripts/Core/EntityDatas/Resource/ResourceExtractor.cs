namespace Core.EntityDatas.Resource
{
    public class ResourceExtractor
    {
        private readonly ResourceData _resourceData;
        private int _level;

        public int Level => _level;
        public ResourceData ResourceData => _resourceData;

        public ResourceExtractor(ResourceData resourceData)
        {
            _resourceData = resourceData;
            _level = 1;
        }

        public void Upgrade(int amount = 1)
        {
            _level += amount;
        }

        public int Extract()
        {
            return _resourceData.BaseExtaction;
        }
    }
}