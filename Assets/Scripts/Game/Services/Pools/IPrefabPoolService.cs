using Ecs.Views.Linkable;
using UnityEngine;

namespace Game.Services.Pools
{
    public interface IPrefabPoolService
    {
        bool Spawn(string prefab, Vector3 position, Quaternion rotation, out IObjectLinkable linkable);
    }
}