using UI.HeroMenu.Views;
using UnityEditor;
using UnityEngine;

namespace SO.Configs
{
    [CreateAssetMenu(menuName = "Config containers/Pool/Heros", fileName = "AvailableHerosPoolConfig")]
    public class AvailableHerosPoolConfig : ScriptableObject
    {
        [field: SerializeField] public HeroItemView HeroItemViewPrefab { get; private set; }
        [field: SerializeField] public int InitCount { get; private set; }
        [field: SerializeField] public Transform Container {  get; private set; }
    }
}