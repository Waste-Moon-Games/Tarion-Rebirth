using UnityEngine;

namespace Core.GameStates
{
    public class GameWorldState
    {
        [field: SerializeField] public ImperiumState ImperiumState { get; private set; }

        public GameWorldState()
        {
            ImperiumState = new ImperiumState();
        }
    }
}