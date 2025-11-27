using TMPro;
using UI.Base;
using UnityEngine;

namespace UI.MissionContexUI
{
    public class MissionPreparationUI : UIListBase
    {
        [field: SerializeField] public TextMeshProUGUI DifficultyText { get; private set; }
        [field: SerializeField] public TextMeshProUGUI DurationText { get; private set; }
    }
}