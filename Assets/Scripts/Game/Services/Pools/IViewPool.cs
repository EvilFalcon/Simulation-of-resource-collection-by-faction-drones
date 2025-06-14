using System;
using Ecs.Managers;

namespace Game.Services.Pools
{
    public interface IViewPool<TObject>
    {
        public IObservable<Uid> OnViewOfEntityDestroyed { get; }
    }
}