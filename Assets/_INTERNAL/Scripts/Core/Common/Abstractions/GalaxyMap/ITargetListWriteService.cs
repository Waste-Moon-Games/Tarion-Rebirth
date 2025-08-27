using GameEntity.DataInstance;

namespace Core.Common.Abstractions.GalaxyMap
{
    public interface ITargetListWriteService
    {
        void AddPlanetToTarget(PlanetInstance planet);
    }
}