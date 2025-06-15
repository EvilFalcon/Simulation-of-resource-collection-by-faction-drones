using UnityEngine;

namespace Ecs.Utils.Repositories
{
    public interface IActiveResourcesRepository
    {
        void Add(GameEntity entity);
        GameEntity Get(Vector3 position);
        GameEntity GetNearestEntity(Vector3 position);
        void Clear();
    }
}