using System;
using UnityEngine;

namespace Core.Common.SimpleTimer
{
    public class Timer : ISimpleTimer
    {
        private float _duration;
        private float _remainingTime;

        private bool _isRunning;

        public float Progress => Mathf.Clamp01(_remainingTime / _duration);

        public bool IsRunning => _isRunning;

        public event Action OnTimeEnded;
        public event Action<float> OnProgressUpdated;

        public void Initialize(float duration)
        {
            _duration = Mathf.Max(duration, 0.01f);
            _remainingTime = _duration;
            _isRunning = false;
        }

        public void Reset()
        {
            _isRunning = false;
        }

        public void Start()
        {
            _isRunning = true;
        }

        public void Stop()
        {
            _isRunning = false;
        }

        public void Tick()
        {
            if (!_isRunning) return;

            _remainingTime -= Time.deltaTime;
            OnProgressUpdated?.Invoke(Progress);

            if (_remainingTime <= 0f)
            {
                OnTimeEnded?.Invoke();
                Stop();
            }
        }
    }
}
