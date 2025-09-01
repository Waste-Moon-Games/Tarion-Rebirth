using UnityEngine;

namespace Core.GameStates
{
    public class ImperiumState
    {
        [field: SerializeField] public TargetsListState TargetsListState { get; private set; }

        public ImperiumState()
        {
            TargetsListState = new TargetsListState();
        }
    }
}