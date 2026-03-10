using Core.Common.MVVM;
using Core.GameStates;
using GameEntity.DataInstance.Main;

namespace UI.HeroMenu.Models
{
    public class HeroMenuModel : IModel
    {
        //private readonly StatsModel _statsModel;
        private readonly HeroBarracksModel _barracksModel;
        private readonly RecruitHerosModel _recruitModel;

        public HeroBarracksModel HeroBarracksModel => _barracksModel;
        public RecruitHerosModel RecruitHerosModel => _recruitModel;

        public HeroMenuModel(ImperiumInstancesHolder instancesHolder, ImperiumState imperiumState)
        {
            _barracksModel = new(instancesHolder);
            _recruitModel = new(imperiumState);
        }

        public void ToggleBarracks()
        {
            _barracksModel.Open();
            _recruitModel.Close();
        }

        public void ToggleRecruit()
        {
            _recruitModel.Open();
            _barracksModel.Close();
        }
    }
}