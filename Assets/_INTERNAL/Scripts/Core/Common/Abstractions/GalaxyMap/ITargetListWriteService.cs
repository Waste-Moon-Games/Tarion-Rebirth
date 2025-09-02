using Core.Common.Instances;

namespace Core.Common.Abstractions.GalaxyMap
{
    public interface ITargetListWriteService
    {
        void AddPlanetToTarget(IInstance planet);
    }
}