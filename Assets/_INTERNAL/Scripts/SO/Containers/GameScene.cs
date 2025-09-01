using UnityEngine;

namespace SO.Containers
{
    [CreateAssetMenu(menuName = "Scene SO/SceneName Prefab", fileName = "Game Scene")]
    public class GameScene : ScriptableObject
    {
        [field: SerializeField] public string SceneName { get; private set; }
    }
}