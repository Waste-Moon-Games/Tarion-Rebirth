using GameEntity.DataInstance;
using Mono.UI.HeroListUI;
using Scripts.GameEntity.DataInstance;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;

namespace UI.HeroRecruitingUI
{
    public class HeroItemSpawner : MonoBehaviour
    {
        [SerializeField] private Transform _content;
        [SerializeField] private HeroItemView _itemPrefab;
        [SerializeField] private bool _autoExpand;

        private ObjectPool<HeroItemView> _itemsPool;

        public void CreatePool(int count)
        {
            _itemsPool = new(_itemPrefab, count, _content)
            {
                AutoExpand = _autoExpand
            };
        }

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