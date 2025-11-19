using Core.Common.Abstractions;
using Core.Common.Instances;
using R3;

namespace Core.Common
{
    public interface IController : IInstanceWriteService
    {
        Observable<IInstance> InstanceAdded { get; }
    }
}