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

        private MissionType _missionType;

        private Button _selectButton;

        public event Action<MissionType> OnMissionTypeSelected;

        private void OnDisable()
        {
            _selectButton.onClick?.RemoveListener(() => OnMissionTypeSelected?.Invoke(_missionType));
        }

        public void Setup(MissionType missionType)
        {
            _missionType = missionType;

            _missionTypeText.text = $"{_missionType}";

            _selectButton = GetComponent<Button>();
            _selectButton.onClick.AddListener(() => OnMissionTypeSelected?.Invoke(_missionType));
        }
    }
}