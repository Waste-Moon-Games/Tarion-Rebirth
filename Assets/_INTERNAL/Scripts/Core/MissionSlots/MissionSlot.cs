using Contex.MissionInfo;
using Core.Factories.Stage_Factory;
using Mono.StateMachine;
using Stages.StageController;
using System;

namespace Core.MissionSlots
{
    public class MissionSlot
    {
        private readonly int _slotID;
        private GameStageController _controller;
        private StageDependencies _deps;
        private MissionContex _contex;

        public int SlotID => _slotID;
        public GameStageController Controller => _controller;
        public MissionContex Contex => _contex;
        public bool IsRunning => _controller != null;
        public bool IsStarted {  get; private set; }

        public event Action<MissionContex> OnMissionStarted;
        public event Action<MissionSlot> OnMissionFinished;

        public MissionSlot(int slotID)
        {
            _slotID = slotID;
        }

        public void AssignMission(StageDependencies deps)
        {
            _deps = deps;
            _contex = deps.MissionContex;
            _controller = new GameStageController(new StageFactory(_deps), _slotID);

            _controller.OnResultAccepted += HandleAcceptedResult;
            _controller.OnMissionStarted += HandleStartedMission;
            _controller.StartCycle();
        }

        public void RefreshDeps(StateMachineUIDependencies uiDeps)
        {
            _deps.RefreshUIDependencies(uiDeps);
            _controller?.RefreshDeps(_deps);
        }

        public void ForceEnd()
        {
            _controller?.ForceEnd();
            Clear();
        }

        public void Update()
        {
            _controller?.Update();
        }

        private void Clear()
        {
            IsStarted = false;
            _controller = null;
        }

        private void HandleStartedMission()
        {
            IsStarted = true;
            OnMissionStarted?.Invoke(_contex);
        }

        private void HandleAcceptedResult()
        {
            OnMissionFinished?.Invoke(this);
            _controller.OnResultAccepted -= HandleAcceptedResult;
            _controller.OnMissionStarted -= HandleStartedMission;
            Clear();
        }
    }
}