using Core.Instances.RecruitSystem;
using Mono.UI.HeroListUI;
using Scripts.GameEntity.DataInstance;
using System;
using System.Collections.Generic;
using UI.HeroDetailInfoUI;
using UI.HeroListUI;
using UnityEngine;
using UnityEngine.UI;

namespace UI.HeroRecruitingUI
{
    public class RecruitingUI : MonoBehaviour
    {
        [Header("UI systems")]
        [SerializeField] private CandidateHeroListController _herosList;
        [SerializeField] private HeroDetailUI _heroDetailUI;
        [SerializeField] private HeroStatsView _heroStatsView;
        [SerializeField] private HeroCost _cost;

        [Space(10), Header("Buttons")]
        [SerializeField] private Button _refreshButton;

        public event Action OnListRefreshed;
        public event Action<HeroInstance> OnHireButtonClicked;

        private void Awake()
        {
            if (_herosList.HeroItems.Count == 0)
                OnListRefreshed?.Invoke();
        }

        private void OnEnable()
        {
            if(_refreshButton != null)
                _refreshButton.onClick.AddListener(RefreshList);

            _heroDetailUI.OnHireButtonClicked += HandleHireButtonClick;
        }

        private void OnDisable()
        {
            if (_refreshButton != null)
                _refreshButton.onClick.RemoveListener(RefreshList);

            _heroDetailUI.OnHireButtonClicked -= HandleHireButtonClick;
        }

        public void SetNewCandidates(List<HeroItemView> newHeros, RecruitSystemInstance instanceHolder)
        {
            _herosList.Clear();
            _herosList.Initialize(instanceHolder, newHeros);

            foreach (var view in newHeros)
            {
                view.OnHeroSelected -= HandleSelectedHero;
                view.OnHeroSelected += HandleSelectedHero;
                _herosList.AddNewItemToList(view);
            }

            if (!_herosList.gameObject.activeSelf)
                _herosList.Show();
        }

        public void RemoveCandidate(HeroInstance candidate)
        {
            _herosList.RemoveItemFromList(candidate);
            _heroDetailUI.Clear();
            _heroStatsView.Clear();
            _cost.Clear();
        }

        private void RefreshList()
        {
            _heroDetailUI.Clear();
            _heroStatsView.Clear();
            OnListRefreshed?.Invoke();
        }

        private void HandleSelectedHero(HeroInstance selectedHero)
        {
            _heroDetailUI.Setup(selectedHero);
            _heroStatsView.Setup(selectedHero);
            _cost.Setup(selectedHero);
        }

        private void HandleHireButtonClick(HeroInstance newHero)
        {
            OnHireButtonClicked?.Invoke(newHero);
        }
    }
}