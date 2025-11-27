namespace Core.Common.MVVM
{
    public interface IViewModel
    {
        void BindModel(IModel model);
        void Dispose();
    }
}