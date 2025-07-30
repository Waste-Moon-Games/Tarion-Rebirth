using UnityEngine;
using UnityEngine.UI;

namespace Entry.Mono.MissionPanel
{
    public class MissionUIToggler : MonoBehaviour
    {
        [SerializeField] private GameObject _missionPanel;

        private Button _toggleButton;

        private void OnEnable()
        {
            if(_toggleButton == null)
            {
                _toggleButton = GetComponent<Button>();
            }

            _toggleButton.onClick.AddListener(ToggleMissionPanel);
        }

        private void OnDisable()
        {
            _toggleButton.onClick.RemoveListener(ToggleMissionPanel);
        }

        private void ToggleMissionPanel()
        {
            if (_missionPanel != null && _toggleButton != null)
            {
                _missionPanel.SetActive(!_missionPanel.activeSelf);
            }
        }
    }
}