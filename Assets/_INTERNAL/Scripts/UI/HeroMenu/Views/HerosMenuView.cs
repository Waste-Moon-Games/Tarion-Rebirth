using GameEntity.DataInstance.Main;
using UI.HeroDetailInfoUI;
using UnityEngine;

namespace UI.HeroUI
{
    public class HerosMenuView : MonoBehaviour
    {
        public void Bind(in ImperiumInstancesHolder holder, in OwnedHeroInfoHolderView ownedHeroInfoHolderView)
        {
            ownedHeroInfoHolderView.Init(holder);
        }

        public void AttachView(GameObject view)
        {
            view.transform.SetParent(transform, false);
        }
    }
}