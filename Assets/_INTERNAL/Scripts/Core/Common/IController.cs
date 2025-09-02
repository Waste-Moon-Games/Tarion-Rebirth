using Core.Common.Abstractions;
using Core.Common.Instances;
using System;

namespace Core.Common
{
    public interface IController : IMapWriteService
    {
        event Action<IInstance> OnInstanceSelected;
    }
}