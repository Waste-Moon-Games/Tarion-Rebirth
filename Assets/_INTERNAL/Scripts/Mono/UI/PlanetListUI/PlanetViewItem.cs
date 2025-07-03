using GameEntity.DataInstance;
using GameEntity.DataInstance.Main;
using TMPro;
using UnityEngine;

namespace Scripts.Mono.UI.PlanetListUI
{
    public class PlanetViewItem : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _planetNameText;

        private PlanetInstance _planetInstance;

        public void Setup(PlanetInstance instanceHolder)
        {
            _planetInstance = instanceHolder;
            _planetNameText.text = _planetInstance.RuntimeData.PlanetName;
        }
    }
}