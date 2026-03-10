using UI.HeroMenu.AdditionalViews;
using UnityEditor;
using UnityEngine;

namespace SO.Configs
{
    [CreateAssetMenu(menuName = "Config containers/Pool/Available Heros", fileName = "AvailableHerosPoolConfig")]
    public class AvailableHerosPoolConfig : ScriptableObject
    {
        [field: SerializeField] public HeroItemView SimpleItemViewPrefab { get; private set; }
        [field: SerializeField] public HeroItemView EnchantedItemViewPrefab { get; private set; }
        [field: SerializeField, Range(1, 100)] public int InitCount { get; private set; }
    }
}