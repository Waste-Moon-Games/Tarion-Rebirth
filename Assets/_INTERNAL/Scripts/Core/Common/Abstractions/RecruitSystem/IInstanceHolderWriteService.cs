using Core.Common.Instances;

namespace Core.Common.Abstractions.RecruitSystem
{
    public interface IInstanceHolderWriteService
    {
        void AddNewInstance(IInstance instance);
    }
}