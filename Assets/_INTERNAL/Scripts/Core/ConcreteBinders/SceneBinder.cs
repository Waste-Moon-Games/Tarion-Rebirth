using Core.Common;
using System.Collections.Generic;

namespace Core.ConcreteBinders
{
    public class SceneBinder : IDisposable
    {
        private readonly List<ISceneBinder> _binders = new();

        public void AddBinder(ISceneBinder newBinder)
        {
            newBinder.Bind();
            _binders.Add(newBinder);
        }

        public void Dispose()
        {
            foreach(ISceneBinder binder in _binders)
                binder.Dispose();

            _binders.Clear();
        }
    }
}