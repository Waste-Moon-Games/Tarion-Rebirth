using UnityEngine;

namespace UI.HeroDetailInfoUI
{
    public class SwitchPanelsInHeroUI : MonoBehaviour
    {
        [SerializeField] private OwnedHeroInfoHolderView _ownedHeros;
        [SerializeField] private GameObject _candidates;

        [Space(10), Header("Buttons")]
        [SerializeField] private ToggleButton _ownedHerosButton;
        [SerializeField] private ToggleButton _candidateHerosButton;

        private void OnEnable()
        {
            _ownedHerosButton.OnButtonClicked += HandleOwnedButtonClicked;
            _candidateHerosButton.OnButtonClicked += HandleCandidateButtonClicked;
        }

        private void OnDisable()
        {
            _ownedHerosButton.OnButtonClicked -= HandleOwnedButtonClicked;
            _candidateHerosButton.OnButtonClicked -= HandleCandidateButtonClicked;
        }

        private void HandleOwnedButtonClicked()
        {
            if(_candidates.activeSelf)
                _candidates.SetActive(false);
            if (!_ownedHeros.gameObject.activeSelf)
                _ownedHeros.Show();
        }

        private void HandleCandidateButtonClicked()
        {
            if(_ownedHeros.gameObject.activeSelf)
                _ownedHeros.Hide();
            if (!_candidates.activeSelf)
                _candidates.SetActive(true);
        }
    }
}