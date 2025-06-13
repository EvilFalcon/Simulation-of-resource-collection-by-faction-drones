using UnityEngine;

namespace Ecs.Utils
{
	public readonly struct InteractedInfo
	{
		public readonly Vector3 HitPoint;
		public readonly Vector3 HitNormal;
		public readonly float Distance;
		public readonly int ObjectHash;
		public readonly Transform Transform;

		public InteractedInfo(
			Vector3 hitPoint,
			Vector3 hitNormal,
			float distance,
			int objectHash,
			Transform transform = null
		)
		{
			HitPoint = hitPoint;
			HitNormal = hitNormal;
			Distance = distance;
			ObjectHash = objectHash;
			Transform = transform;
		}
	}
}