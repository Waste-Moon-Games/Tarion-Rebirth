using Contex.MissionInfo;
using Core.Common.SimpleTimer;
using System;
using UI.Base;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MissionExecutionUI
{
    public class MissionExecutionTimer : SimpleUIItem
    {
        [SerializeField] private Image _missionProgress;

        private ISimpleTimer _timer;

        public event Action OnTimeEnded;

        public void Initialize(MissionContex missionContex)
        {
            _timer = new Timer();
            _timer.Initialize(missionContex.PreparedMission.Duration);
        }

        public void StartTimer()
        {
            _timer.OnTimeEnded += HandleEndedTime;
            _timer.Start();
            _missionProgress.fillAmount = _timer.Progress;
        }

        public void UpdateTimer()
        {
            _timer.Tick();
            _missionProgress.fillAmount = _timer.Progress;
        }

        public void StopTimer()
        {
            _timer.OnTimeEnded -= HandleEndedTime;
            _timer.Stop();
            _missionProgress.fillAmount = _timer.Progress;
        }

        private void HandleEndedTime()
        {
            OnTimeEnded?.Invoke();
        }
    }
}