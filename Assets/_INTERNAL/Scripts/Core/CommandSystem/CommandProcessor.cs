using Core.Common.Command;
using System.Collections.Generic;

namespace Core.CommandSystem
{
    public class CommandProcessor
    {
        private readonly Queue<ICommand> _comands = new();

        public void AddCommand(ICommand command)
        {
            _comands.Enqueue(command);
        }

        public void Process()
        {
            while (_comands.Count > 0)
            {
                var command = _comands.Dequeue();
                command.Execute();
            }
        }
    }
}