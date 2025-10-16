using Scripts.GameEntity.DataInstance;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Utils.Formatter;

namespace Mono.UI.HeroListUI
{
    public class HeroItemView : MonoBehaviour
    {
        [Header("Text")]
        [SerializeField] private TextMeshProUGUI _heroName;
        [SerializeField] private TextMeshProUGUI _heroPower;
        [SerializeField] private TextMeshProUGUI _heroLevel;

        [Space(10), Header("Sprite")]
        [SerializeField] private Image _heroArt;

        private Button _selectButton;

        private HeroInstance _heroInstance;
        private NumberFormatter _formatter;

        private UnityAction _clickHandler;

        public HeroInstance Hero => _heroInstance;
        public Button SelectButton => _selectButton;

        public event Action<HeroInstance> OnHeroSelected;

        private void OnDisable()
        {
            if(_clickHandler != null)
                _selectButton.onClick.RemoveListener(_clickHandler);

            if(_heroInstance != null)
                _heroInstance.OnPowerChanged -= HandleChangedPower;
        }

        public void Setup(HeroInstance heroInstance)
        {
            _formatter ??= new();

            _heroInstance = heroInstance;
            _heroInstance.OnPowerChanged += HandleChangedPower;
            SetupText(_heroInstance);

            if (_heroArt == null)
                return;

            if(heroInstance.RuntimeData.HeroArt != null)
                _heroArt.sprite = heroInstance.RuntimeData.HeroArt;
        }

        public void InitializeButton()
        {
            if(_selectButton == null)
                _selectButton = GetComponent<Button>();

            if (_clickHandler != null)
                _selectButton.onClick.RemoveListener(_clickHandler);

            _clickHandler = () => OnHeroSelected?.Invoke(_heroInstance);
            _selectButton.onClick.AddListener(_clickHandler);
        }

        public void Clear()
        {
            _heroInstance = null;
            _heroName.text = string.Empty;
            _heroPower.text = string.Empty;
            _heroLevel.text = string.Empty;
            gameObject.SetActive(false);
        }

        private void SetupText(HeroInstance hero)
        {
            _heroName.text = hero.RuntimeData.Name;
            _heroPower.text = $"Мощь: {_formatter.FormatNumber(hero.HeroPower)}";
            _heroLevel.text = $"LVL: {hero.HeroLevel}";
        }

        private void HandleChangedPower(float currentPower)
        {
            _heroPower.text = $"Мощь: {_formatter.FormatNumber(currentPower)}";
        }
    }
}