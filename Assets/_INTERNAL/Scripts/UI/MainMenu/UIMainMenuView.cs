using System.Collections.Generic;
using UI.MainMenu.MissionExecutionUI;
using UnityEngine;
using UnityEngine.UI;

namespace UI.MainMenu
{
    public class UIMainMenuView : MonoBehaviour
    {
        [Header("Buttons")]
        [field: SerializeField] public Button GoToMapButton { get; private set; }
        [field: SerializeField] public Button GoToHerosButton { get; private set; }
        [field: SerializeField] public Button ToggleMissionPreparationButton { get; private set; }
        [field: SerializeField] public List<MissionExecutionTimer> Timers { get; private set; }

        private GameObject _window;

        public void Clear()
        {
            Timers.Clear();
            _window = null;
            GoToHerosButton = null;
            GoToMapButton = null;
            ToggleMissionPreparationButton = null;
        }

        public void SetMissionWindow(GameObject window)
        {
            _window = window;
        }

        public void TogglePreparationWindow()
        {
            _window.SetActive(!_window.activeSelf);
        }
    }
}