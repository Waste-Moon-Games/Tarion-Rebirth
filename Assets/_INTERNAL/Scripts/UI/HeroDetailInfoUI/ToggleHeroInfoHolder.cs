using UnityEngine;
using UnityEngine.UI;

namespace HeroDetailInfoUI
{
    public class ToggleHeroInfoHolder : MonoBehaviour
    {
        [SerializeField] private GameObject _infoHolder;

        private Button _toggleButton;

        private void OnEnable()
        {
            if (_toggleButton == null)
                _toggleButton = GetComponent<Button>();

            _toggleButton.onClick.AddListener(ToggleInfoHolder);
        }

        private void OnDisable()
        {
            _toggleButton.onClick.RemoveListener(ToggleInfoHolder);
        }

        private void ToggleInfoHolder()
        {
            _infoHolder.SetActive(!_infoHolder.activeSelf);
        }
    }
}