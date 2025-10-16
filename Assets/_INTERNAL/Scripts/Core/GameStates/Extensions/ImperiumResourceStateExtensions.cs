using Core.EntityDatas.Resource;
using R3;
using System;

namespace Core.GameStates.Extensions
{
    public static class ImperiumResourceStateExtensions
    {
        public static Observable<(ResourceType type, int value)> OnResourceChangedAsObservable(this ImperiumResourceState resource)
        {
            return Observable.FromEvent<Action<ResourceType, int>, (ResourceType, int)>
                (
                handler => (t, v) => handler((t, v)),
                h => resource.OnResourceChanged += h,
                h => resource.OnResourceChanged -= h
                );
        }
    }
}