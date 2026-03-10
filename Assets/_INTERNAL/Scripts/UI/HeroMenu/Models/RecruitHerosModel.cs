using Core.Common.MVVM;
using Core.EntityDatas.Resource;
using Core.EntityGenerationSystem;
using Core.GameStates;
using Core.Instances.RecruitSystem;
using GameEntity.DataInstance.Main;
using GameEntity.Unit.Data;
using R3;
using Scripts.GameEntity.DataInstance;
using SO.Containers.Configs;
using SO.Containers.GameEntity;
using System.Collections.Generic;
using UnityEngine;

namespace UI.HeroMenu.Models
{
    public class RecruitHerosModel : IModel
    {
        private readonly Subject<bool> _stateChangedSignal = new();
        private readonly Subject<Unit> _refreshSignal = new();
        private readonly Subject<HeroInstance> _recruitCanceledSignal = new();
        private readonly Subject<HeroInstance> _heroRecruitedSignal = new();
        private readonly Subject<HeroInstance> _selectedHeroSignal = new();
        private readonly Subject<List<HeroInstance>> _generatedHerosSignal = new();

        private readonly HeroGenerator _generator;
        private readonly RecruitSystemInstance _localInstanceHolder;
        private readonly HerosGenerationConfig _config;

        private readonly ImperiumInstancesHolder _globalInstanceHolder;
        private readonly ImperiumResourceState _imperiumResources;

        private bool _state = true;

        public Observable<bool> StateChanged => _stateChangedSignal.AsObservable();
        public Observable<List<HeroInstance>> GeneratedHeros => _generatedHerosSignal.AsObservable();
        public Observable<HeroInstance> HeroRecruited => _heroRecruitedSignal.AsObservable();
        public Observable<HeroInstance> HeroRecruitedCancel => _recruitCanceledSignal.AsObservable();
        public Observable<HeroInstance> HeroSelected => _selectedHeroSignal.AsObservable();
        public Observable<Unit> Refreshed => _refreshSignal.AsObservable();

        public RecruitHerosModel(ImperiumState imperiumState)
        {
            _imperiumResources = imperiumState.ImperiumResources;
            _globalInstanceHolder = imperiumState.InstanceHolder;

            RankProgressionConfig rankProgressionConfig = Resources.Load<RankProgressionConfig>("Configs/Heros/Rank/RankConfig");
            HerosGenerationConfig config = Resources.Load<HerosGenerationConfig>("Configs/Heros/HerosGenerationConfig");

            _localInstanceHolder = new(rankProgressionConfig);
            _generator = new(config, imperiumState.InstanceHolder);
            _config = config;
        }

        /// <summary>
        /// Открыть Найм
        /// </summary>
        public void Open()
        {
            _state = true;
            _stateChangedSignal.OnNext(_state);
        }

        /// <summary>
        /// Закрыть Найм
        /// </summary>
        public void Close()
        {
            _state = false;
            _stateChangedSignal.OnNext(_state);
        }

        /// <summary>
        /// Обновить Список найма
        /// </summary>
        public void RefreshRecruitList()
        {
            List<HeroData> newHeros = _generator.GenerateHeros(_config.Count);
            _localInstanceHolder.SetGeneratedHeros(newHeros);

            _generatedHerosSignal.OnNext(_localInstanceHolder.Heros);
            _refreshSignal.OnNext(Unit.Default);
        }

        /// <summary>
        /// Задать выбранного героя
        /// </summary>
        /// <param name="selectedHero"></param>
        public void SetSelectedHero(HeroInstance selectedHero) => _selectedHeroSignal.OnNext(selectedHero);

        /// <summary>
        /// Нанять Героя
        /// </summary>
        /// <param name="newHero"></param>
        public void RecruitHero(HeroInstance newHero)
        {
            int cost = Mathf.RoundToInt(newHero.RuntimeData.RecruitmentCost);
            if(cost < newHero.RuntimeData.RecruitmentCost)
            {
                _recruitCanceledSignal.OnNext(newHero);
                return;
            }

            _imperiumResources.Spend(ResourceType.Void_Matter, cost);
            _globalInstanceHolder.AddNewInstance(newHero);
            _heroRecruitedSignal.OnNext(newHero);
        }
    }
}