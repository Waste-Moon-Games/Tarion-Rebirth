using Core.Common.Instances;

namespace Core.Common.Abstractions.GalaxyMap
{
    public interface ITargetListWriteService
    {
        void AddTarget(IInstance target);
        void RemoveTarget(IInstance target);
        void ClearTargets();
    }
}