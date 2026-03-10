using Scripts.GameEntity.DataInstance;
using System.Collections.Generic;
using System.Linq;
using UI.HeroMenu.AdditionalViews;
using UnityEngine;
using Utils;

namespace UI.HeroMenu.Services
{
    public class HeroItemSpawner
    {
        private ObjectPool<HeroItemView> _itemsPool;

        public void SetPool(ObjectPool<HeroItemView> herosPool) => _itemsPool = herosPool;

        public List<HeroItemView> SpawnHeros(List<HeroInstance> heros)
        {
            Clear();

            foreach (HeroInstance hero in heros)
            {
                HeroItemView view = _itemsPool?.GetFreeElement();

                view.Setup(hero);
                view.InitializeButton();
            }

            return _itemsPool.GetFreeElements();
        }

        public void RemoveCandidate(HeroInstance candidate)
        {
            HeroItemView view = _itemsPool?.FirstOrDefault(c => c.Hero == candidate);

            if (view != null)
                _itemsPool.ReturnToPool(view);
        }

        private void Clear()
        {
            foreach (HeroItemView hero in _itemsPool)
            {
                _itemsPool.ReturnToPool(hero);
            }
        }
    }
}