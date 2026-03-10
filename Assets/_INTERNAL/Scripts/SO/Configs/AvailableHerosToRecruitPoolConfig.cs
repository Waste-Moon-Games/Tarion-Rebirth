using UI.HeroMenu.AdditionalViews;
using UnityEditor;
using UnityEngine;

namespace SO.Configs
{
    [CreateAssetMenu(menuName = "Config containers/Pool/Heros Recruit", fileName = "AvailableHerosToRecruitPoolConfig")]
    public class AvailableHerosToRecruitPoolConfig : ScriptableObject
    {
        [field: SerializeField] public HeroItemView HeroTabPrefab { get; private set; }
        [field: SerializeField, Range(1, 100)] public int InitCount { get; private set; }
    }
}