using System;

namespace Core.Common.SimpleTimer
{
    public interface ISimpleTimer
    {
        event Action OnTimeEnded;

        void Initialize(float startTime);
        void Start();
        void Tick();
        void Stop();
        void Reset();

        float Progress { get; }
        bool IsRunning { get; }
    }
}