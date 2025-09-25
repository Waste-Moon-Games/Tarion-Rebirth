using Core.Common.Instances;

namespace Core.Common.Abstractions
{
    public interface IInstanceWriteService
    {
        void RemoveInstance(IInstance instance);
    }
}