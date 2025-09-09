using Core.GameStates;
using Entry.Mono;
using UnityEngine;

namespace UI.GameUIBridges
{
    public class ImperiumStateUIBridge : MonoBehaviour
    {
        [field: SerializeField] public ImperiumState ImperiumState { get; private set; }

        public bool HasImperiumState => ImperiumState != null;

        private void Awake()
        {
            ImperiumState = GameWorldStateMono.Instance.GameWorldState?.ImperiumState;
        }
    }
}