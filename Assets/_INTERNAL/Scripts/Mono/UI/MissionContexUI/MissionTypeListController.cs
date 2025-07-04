using GameEntity.DataInstance;
using GameEntity.DataInstance.Main;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Mono.UI.MissionContexUI
{
    public class MissionTypeListController : UIListBase
    {
        [SerializeField] private Transform _contentParent;
        [SerializeField] private MissionTypeItemView _missionTypeItemPrefab;
        [SerializeField] private List<MissionTypeItemView> _missionItems = new();

        private InstanceHolder _instanceHolder;

        public event Action<MissionInstance> OnMissionTypeSelected;

        public void Initialize(InstanceHolder instanceHolder)
        {
            _instanceHolder = instanceHolder;
            GenerateMissionTypeList();
        }

        private void OnDisable()
        {
            foreach (var mission in _missionItems)
            {
                mission.OnMissionTypeSelected -= HandleSelectedMissionType;
            }
        }

        private void GenerateMissionTypeList()
        {
            foreach (Transform child in _contentParent)
            {
                Destroy(child.gameObject);
            }
            _missionItems.Clear();

            foreach (var mission in _instanceHolder.Missions)
            {
                var itemGO = Instantiate(_missionTypeItemPrefab, _contentParent);
                var item = itemGO.GetComponent<MissionTypeItemView>();

                _missionItems.Add(item);

                item.Setup(mission);

                item.OnMissionTypeSelected += HandleSelectedMissionType;
            }
        }

        private void HandleSelectedMissionType(MissionInstance missionType)
        {
            OnMissionTypeSelected?.Invoke(missionType);
        }
    }
}