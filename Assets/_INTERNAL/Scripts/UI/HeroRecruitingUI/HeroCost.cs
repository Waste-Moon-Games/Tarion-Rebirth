using Scripts.GameEntity.DataInstance;
using TMPro;
using UI.Base;
using UnityEngine;
using Utils.Formatter;

namespace UI.HeroRecruitingUI
{
    public class HeroCost : SimpleUIItem
    {
        [SerializeField] private TextMeshProUGUI _costText;

        private NumberFormatter _formatter;

        private void Awake()
        {
            if (gameObject.activeSelf)
                Hide();

            _formatter ??= new();
        }

        public void Setup(HeroInstance selectedHero)
        {
            Show();
            _costText.text = _formatter.FormatNumber(selectedHero.RuntimeData.RecruitmentCost);
        }

        public void Clear()
        {
            _costText.text = string.Empty;
            Hide();
        }
    }
}