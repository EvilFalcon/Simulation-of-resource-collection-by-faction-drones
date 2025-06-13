using UnityEngine;

namespace Game.Utils
{
    public static class TransformExtensions
    {
        public static void SetFarAwayPosition(this Transform transform, Vector3 additional = default)
        {
            transform.position = new Vector3(10000, 10000, 10000);

            if (additional != default)
                transform.position += additional;
        }
    }
}