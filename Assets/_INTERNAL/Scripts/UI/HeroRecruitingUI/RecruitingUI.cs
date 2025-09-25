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

        [Space(10), Header("Buttons")]
        [SerializeField] private Button _refreshButton;

        public event Action OnListRefreshed;
        public event Action<HeroInstance> OnHeroRecruited;

        private void Awake()
        {
            if (_herosList.HeroItems.Count == 0)
                OnListRefreshed?.Invoke();
        }

        private void OnEnable()
        {
            if(_refreshButton != null)
                _refreshButton.onClick.AddListener(RefreshList);

            _heroDetailUI.OnHeroRecuired += HandleAddedHero;
        }

        private void OnDisable()
        {
            if (_refreshButton != null)
                _refreshButton.onClick.RemoveListener(RefreshList);

            _heroDetailUI.OnHeroRecuired -= HandleAddedHero;
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
        }

        private void RefreshList()
        {
            _heroDetailUI.Clear();
            OnListRefreshed?.Invoke();
        }

        private void HandleSelectedHero(HeroInstance selectedHero)
        {
            _heroDetailUI.Setup(selectedHero);
            _heroStatsView.Setup(selectedHero);
        }

        private void HandleAddedHero(HeroInstance newHero)
        {
            OnHeroRecruited?.Invoke(newHero);
        }
    }
}