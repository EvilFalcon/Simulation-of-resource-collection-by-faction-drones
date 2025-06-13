using Ecs.Views.Linkable;
using Zenject;

namespace Game.Services.Pool
{
    public interface IPrefabMemoryPool : IMemoryPool<IObjectLinkable>
    {
        string Name { get; }
    }
}