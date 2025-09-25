using Core.Common.Abstractions;
using Core.Common.Abstractions.GalaxyMap;
using Core.Common.Command;
using GameEntity.DataInstance;
using System;

namespace CommandSystem
{
    public class AddPlanetToTargetListCommand : ICommand
    {
        private readonly PlanetInstance _planet;
        private readonly IInstanceWriteService _map;
        private readonly ITargetListWriteService _targetList;

        public AddPlanetToTargetListCommand(PlanetInstance planet, IInstanceWriteService map, ITargetListWriteService targetList)
        {
            _planet = planet ?? throw new ArgumentNullException(nameof(planet));
            _map = map ?? throw new ArgumentNullException(nameof(map));
            _targetList = targetList ?? throw new ArgumentNullException(nameof(targetList));
        }

        public void Execute()
        {
            _map.RemoveInstance(_planet);
            _targetList.AddTarget(_planet);
        }

        public void Undo() { }
    }
}