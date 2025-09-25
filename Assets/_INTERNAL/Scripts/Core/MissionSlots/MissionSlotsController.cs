using Contex.MissionInfo;
using Core.Factories.Stage_Factory;
using Mono.StateMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Core.MissionSlots
{
    public class MissionSlotsController
    {
        [field: SerializeField] public int MaxSlots { get; private set; }

        private readonly List<MissionSlot> _missionSlots = new();

        public IReadOnlyList<MissionSlot> Slots => _missionSlots;
        public int FreeSlots { get; private set; }
        public bool HasFreeSlots => FreeSlots > 0;

        public event Action<MissionContex> OnMissionPreparationStarted;
        public event Action<MissionContex> OnMissionStarted;
        public event Action<int, MissionContex> OnMissionFinished;

        public MissionSlotsController(int slotCount)
        {
            MaxSlots = slotCount;
            FreeSlots = slotCount;
            for(int i = 0; i < slotCount; i++)
                _missionSlots.Add(new MissionSlot(i));
        }

        public void StartPreparateMission(StageDependencies deps)
        {
            var freeSlot = _missionSlots.FirstOrDefault(s => !s.IsRunning);

            if (freeSlot == null)
            {
                Debug.Log("Not enough free slots");
                return;
            }

            freeSlot.AssignMission(deps);
            freeSlot.OnMissionStarted += HandleStartedMission;
            freeSlot.OnMissionFinished += HandleFinishedMission;

            if(HasFreeSlots)
                FreeSlots--;

            OnMissionPreparationStarted?.Invoke(freeSlot.Contex);
        }

        public void RefreshDeps(StateMachineUIDependencies uiDeps)
        {
            foreach (MissionSlot slot in _missionSlots)
            {
                if (slot.IsStarted)
                    slot.RefreshDeps(uiDeps);
            }
        }

        public void Update()
        {
            foreach (MissionSlot slot in _missionSlots)
                slot?.Update();
        }

        public void ForceEnd()
        {
            var slot = _missionSlots.FirstOrDefault(s => !s.IsStarted);
            if (slot == null)
                return;

            slot.OnMissionStarted -= HandleStartedMission;
            slot.OnMissionFinished -= HandleFinishedMission;
            slot.ForceEnd();

            if(FreeSlots < MaxSlots)
                FreeSlots++;

            OnMissionFinished?.Invoke(slot.SlotID, slot.Contex);
        }

        private void HandleStartedMission(MissionContex contex)
        {
            OnMissionStarted?.Invoke(contex);
        }

        private void HandleFinishedMission(MissionSlot slot)
        {
            if (FreeSlots < MaxSlots)
                FreeSlots++;

            OnMissionFinished?.Invoke(slot.SlotID, slot.Contex);
        }
    }
}