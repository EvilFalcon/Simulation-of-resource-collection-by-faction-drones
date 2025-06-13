using UnityEngine;

namespace Db.GameObjectsBase
{
    public interface IPrefabsBase
    {
        GameObject Get(string prefabName);
    }
}