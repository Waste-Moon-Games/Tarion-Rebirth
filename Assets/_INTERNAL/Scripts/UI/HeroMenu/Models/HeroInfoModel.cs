using Core.Common.MVVM;
using GameEntity.DataInstance.Main;

namespace UI.HeroMenu.Models
{
    public class HeroInfoModel : IModel
    {
        //private readonly StatsModel _statsModel;
        //private readonly ListModel _listModel;
        private readonly HeroBarracksModel _barracksModel;
        private readonly RecruitHerosModel _recruitModel;

        public HeroBarracksModel HeroBarracksModel => _barracksModel;
        public RecruitHerosModel RecruitHerosModel => _recruitModel;

        public HeroInfoModel(ImperiumInstancesHolder instancesHolder)
        {
            _barracksModel = new(instancesHolder);
            _recruitModel = new();
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