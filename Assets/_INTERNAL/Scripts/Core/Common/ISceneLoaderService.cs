using R3;

namespace Core.Common
{
    public interface ISceneLoaderService
    {
        Subject<float> OnProgressUpdated { get; }
        Subject<string> OnSceneLoaded { get; }
    }
}