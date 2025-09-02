namespace Core.Common
{
    public interface ISceneBinder : IDisposable
    {
        void Bind();
        void Unbind();
    }
}