using GameEntity.DataInstance.Main;
using UI.HeroDetailInfoUI;
using UnityEngine;

namespace UI
{
    public class HerosMenuView : MonoBehaviour
    {
        [field: SerializeField] public OwnedHeroInfoHolderView OwnedHeroInfoHolderView { get; private set; }

        public void Bind(ImperiumInstancesHolder holder)
        {
            OwnedHeroInfoHolderView.Init(holder);
        }
    }
}