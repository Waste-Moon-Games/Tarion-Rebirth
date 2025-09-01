using Core.GameStates;
using UnityEngine;

namespace Entry.Mono
{
    public class GameWorldStateMono : MonoBehaviour
    {
        public static GameWorldStateMono Instance;

        [field: SerializeField] public GameWorldState GameWorldState { get; private set; }

        private void Awake()
        {
            GameWorldState = new GameWorldState();

            if(Instance == null)
                Instance = this;
        }
    }
}