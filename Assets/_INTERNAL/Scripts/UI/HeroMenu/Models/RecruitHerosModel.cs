using Core.Common.MVVM;
using R3;

namespace UI.HeroMenu.Models
{
    public class RecruitHerosModel : IModel
    {
        private readonly Subject<bool> _stateChangedSignal = new();

        private bool _state = true;

        public Observable<bool> StateChanged => _stateChangedSignal.AsObservable();

        /// <summary>
        /// Открыть Найм
        /// </summary>
        public void Open()
        {
            _state = true;
            _stateChangedSignal.OnNext(_state);
        }

        /// <summary>
        /// Закрыть Найм
        /// </summary>
        public void Close()
        {
            _state = false;
            _stateChangedSignal.OnNext(_state);
        }
    }
}