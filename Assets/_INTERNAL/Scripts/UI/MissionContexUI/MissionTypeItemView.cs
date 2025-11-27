using GameEntity.Mission;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.MissionContexUI
{
    public class MissionTypeItemView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _missionTypeText;

        private MissionType _missionType;

        private Button _selectButton;
        private UnityAction _clickHandler;

        public event Action<MissionType> OnMissionTypeSelected;

        private void OnDisable()
        {
            _selectButton.onClick?.RemoveListener(_clickHandler);
        }

        public void Setup(MissionType missionType)
        {
            _missionType = missionType;

            SetupText(missionType);

            _selectButton = GetComponent<Button>();

            _clickHandler = () => OnMissionTypeSelected?.Invoke(missionType);
            _selectButton.onClick.AddListener(_clickHandler);
        }

        private void SetupText(MissionType missionType)
        {
            switch (missionType)
            {
                case MissionType.Force:
                    _missionTypeText.text = "Захват";
                break;

                case MissionType.Diplomacy:
                    _missionTypeText.text = "Дипломатия";
                break;

                case MissionType.Sabotage:
                    _missionTypeText.text = "Саботаж";
                break;

                case MissionType.Recon:
                    _missionTypeText.text = "Разведка";
                break;
            }
        }
    }
}