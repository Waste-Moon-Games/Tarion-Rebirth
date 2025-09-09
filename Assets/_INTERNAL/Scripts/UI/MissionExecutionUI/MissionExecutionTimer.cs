using Core.Common.SimpleTimer;
using UI.Base;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MissionExecutionUI
{
    public class MissionExecutionTimer : SimpleUIItem
    {
        [Header("UI")]
        [SerializeField] private Image _missionProgress;

        private ISimpleTimer _timer;

        public bool HasTimer => _timer != null;
        public bool HasEnabled => gameObject.activeSelf;

        private void OnDestroy()
        {
            if (_timer != null)
                _timer.OnProgressUpdated -= UpdateTimer;
        }

        public void SetTimer(ISimpleTimer timer)
        {
            _timer = timer;
            _timer.OnProgressUpdated += UpdateTimer;
        }

        public void StartTimer(float value)
        {
            _missionProgress.fillAmount = value;
        }

        public void UpdateTimer(float value)
        {
            if (!_missionProgress)
                return;

            _missionProgress.fillAmount = value;
        }

        public void StopTimer(float value)
        {
            _missionProgress.fillAmount = value;
        }
    }
}