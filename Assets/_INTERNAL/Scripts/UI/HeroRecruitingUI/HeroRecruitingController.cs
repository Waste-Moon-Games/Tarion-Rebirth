using Core.CommandSystem;
using Core.Common;
using Core.Common.Instances;
using Core.ConcreteBinders;
using Core.EntityGenerationSystem;
using Core.GameStates;
using Core.Instances.RecruitSystem;
using GameEntity.Unit.Data;
using Mono.UI.HeroListUI;
using R3;
using Scripts.GameEntity.DataInstance;
using SO.Containers.Configs;
using SO.Containers.GameEntity;
using System.Collections.Generic;
using UnityEngine;

namespace UI.HeroRecruitingUI
{
    public class HeroRecruitingController : MonoBehaviour, IController
    {
        [Header("Generation config")]
        [SerializeField] private HerosGenerationConfig _config;
        [SerializeField] private RankProgressionConfig _rankConfig;

        [Space(10), Header("Spawner")]
        [SerializeField] private HeroItemSpawner _spawner;

        [Space(10), Header("UI")]
        [SerializeField] private RecruitingUI _recruitingUI;

        private ISceneBinder _localBinder;
        private SceneBinder _sceneBinder;

        private ImperiumResourceState _imperiumResources;

        private RecruitSystemInstance _localInsntace;
        private HeroGenerator _generator;
        private CommandProcessor _processor;

        public Observable<IInstance> InstanceAdded { get; }

        private void Awake()
        {
            //var instanceHolder = GameWorldStateMono
            //    .Instance
            //    .GameWorldState
            //    .ImperiumState
            //    .InstanceHolder;
            //_imperiumResources = GameWorldStateMono
            //    .Instance
            //    .GameWorldState
            //    .ImperiumState
            //    .ImperiumResource;

            //_spawner.CreatePool(_config.Count);

            //_generator = new(_config, instanceHolder);
            //_localInsntace = new();

            //_processor = new();

            //_sceneBinder = new();
            //_localBinder = new RecruitHeroSystemBinder
            //    (
            //    this,
            //    instanceHolder,
            //    _processor
            //    );
            //_sceneBinder.AddBinder(_localBinder);
        }

        private void OnEnable()
        {
            _recruitingUI.OnListRefreshed += HandleRefreshedList;
            _recruitingUI.OnHireButtonClicked += HandleAddedHero;
        }

        private void OnDisable()
        {
            _recruitingUI.OnListRefreshed -= HandleRefreshedList;
            _recruitingUI.OnHireButtonClicked -= HandleAddedHero;
        }

        private void Start()
        {
            HandleRefreshedList();
        }

        private void Update()
        {
            _processor?.Process();
        }

        public void RemoveInstance(IInstance hero)
        {
            _recruitingUI.RemoveCandidate(hero as HeroInstance);
            _spawner.RemoveCandidate(hero as HeroInstance);
        }

        private void HandleRefreshedList()
        {
            List<HeroData> newHeros = _generator.GenerateHeros(_config.Count);
            _localInsntace.SetGeneratedHeros(newHeros, _rankConfig);

            List<HeroItemView> spawnedHeros = _spawner.SpawnHeros(_localInsntace.Heros);
            _recruitingUI.SetNewCandidates(spawnedHeros, _localInsntace);
        }

        private void HandleAddedHero(HeroInstance addedHero)
        {
            // to do списать ресы за найм героя
            int cost = Mathf.RoundToInt(addedHero.RuntimeData.RecruitmentCost);
            if (cost < addedHero.RuntimeData.RecruitmentCost)
                return;

            _imperiumResources.Spend(Core.EntityDatas.Resource.ResourceType.Void_Matter, cost);
            //OnInstanceAdded?.OnNext(addedHero);
        }
    }
}