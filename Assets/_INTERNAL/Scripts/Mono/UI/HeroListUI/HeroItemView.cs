using GameEntity.DataInstance;
using Scripts.GameEntity.DataInstance;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Mono.UI.HeroListUI
{
    public class HeroItemView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _heroName;

        private Button _selectButton;

        private HeroInstance _heroInstance;

        public event Action<HeroInstance> OnHeroSelected;

        private void OnDisable()
        {
            _selectButton.onClick.RemoveListener(() => OnHeroSelected?.Invoke(_heroInstance));
        }

        public void Setup(HeroInstance heroInstance)
        {
            _heroInstance = heroInstance;
            _heroName.text = _heroInstance.RuntimeData.Name;

            _selectButton = GetComponent<Button>();

            _selectButton.onClick.AddListener(() => OnHeroSelected?.Invoke(_heroInstance));
        }
    }
}