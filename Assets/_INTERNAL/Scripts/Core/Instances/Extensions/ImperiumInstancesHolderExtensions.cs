using GameEntity.DataInstance.Main;
using R3;
using System;

namespace Core.Instances.Extensions
{
    public static class ImperiumInstancesHolderExtensions
    {
        public static Observable<int> OnPlanetsCountChangedAsObservable(this ImperiumInstancesHolder holder)
        {
            return Observable.FromEvent<Action<int>, int>(
                handler => handler,
                h => holder.OnPlanetsCountChanged += h,
                h => holder.OnPlanetsCountChanged -= h);
        }

        public static Observable<int> OnHerosCountChangedAsObservable(this ImperiumInstancesHolder holder)
        {
            return Observable.FromEvent<Action<int>, int>(
                handler => handler,
                h => holder.OnPlanetsCountChanged += h,
                h => holder.OnPlanetsCountChanged -= h);
        }

        public static Observable<int> OnPlanetsLimitChangedAsObservable(this ImperiumInstancesHolder holder)
        {
            return Observable.FromEvent<Action<int>, int>(
                handler => handler,
                h => holder.OnPlanetsCountChanged += h,
                h => holder.OnPlanetsCountChanged -= h);
        }

        public static Observable<int> OnHerosLimitChangedAsObservable(this ImperiumInstancesHolder holder)
        {
            return Observable.FromEvent<Action<int>, int>(
                handler => handler,
                h => holder.OnPlanetsCountChanged += h,
                h => holder.OnPlanetsCountChanged -= h);
        }
    }
}