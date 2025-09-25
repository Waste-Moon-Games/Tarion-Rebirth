using Core.Common.Abstractions;
using Core.Common.Instances;
using System;

namespace Core.Common
{
    public interface IController : IInstanceWriteService
    {
        event Action<IInstance> OnInstanceSelected;
    }
}