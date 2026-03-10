using Core.CommandSystem;
using Core.Common;
using Core.Common.Instances;
using Core.EntityGenerationSystem;
using Core.Instances.RecruitSystem;
using R3;
using Scripts.GameEntity.DataInstance;
using SO.Containers.Configs;
using SO.Containers.GameEntity;
using UnityEngine;

namespace UI.HeroRecruitingUI
{
    public class HeroRecruitingController : MonoBehaviour, IController
    {
        [Space(10), Header("UI")]
        [SerializeField] private RecruitingUI _recruitingUI;

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

        public void RemoveInstance(IInstance hero)
        {
            _recruitingUI.RemoveCandidate(hero as HeroInstance);
        }
    }
}