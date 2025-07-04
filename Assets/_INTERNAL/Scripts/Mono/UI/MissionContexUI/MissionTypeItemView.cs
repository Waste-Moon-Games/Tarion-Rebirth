using GameEntity.DataInstance;
using GameEntity.Mission;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Mono.UI.MissionContexUI
{
    public class MissionTypeItemView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _missionTypeText;

        private MissionInstance _missionInstance;

        private Button _selectButton;

        public event Action<MissionInstance> OnMissionTypeSelected;

        private void OnDisable()
        {
            _selectButton.onClick?.RemoveListener(() => OnMissionTypeSelected?.Invoke(_missionInstance));
        }

        public void Setup(MissionInstance missionInstance)
        {
            _missionInstance = missionInstance;

            _missionTypeText.text = $"{_missionInstance.RuntimeData.Type}";

            _selectButton = GetComponent<Button>();
            _selectButton.onClick.AddListener(() => OnMissionTypeSelected?.Invoke(_missionInstance));
        }
    }
}