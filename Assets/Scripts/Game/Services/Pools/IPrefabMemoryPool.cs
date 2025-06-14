using Ecs.Views.Linkable;
using Zenject;

namespace Game.Services.Pools
{
    public interface IPrefabMemoryPool : IMemoryPool<IObjectLinkable>
    {
        string Name { get; }
    }
}