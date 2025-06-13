using UnityEngine;

namespace Game.Utils.Raycast
{
	public interface IRayCastProvider
	{
		Vector3 GetMapPositionByRayCast(float x, float z);
		Vector3 GetMapPositionByLayerAndTag(float x, float z, out bool isGroundLayer);

		bool IsPlayerStayOnFloorLayer();

		Vector3 GetHitPoint(
			int layer,
			out Vector3 hitNormal,
			out float distance,
			out int objectHash
		);

		bool GetHitObject(
			Vector3 direction,
			int layer,
			float distance,
			out RaycastHit hit,
			out int objectHash
		);

		bool Raycast(
			Vector3 origin,
			Vector3 direction,
			out RaycastHit hit,
			float distance,
			int layerMask,
			QueryTriggerInteraction triggerInteraction
		);

		bool SphereCast(
			Vector3 origin,
			float radius,
			Vector3 direction,
			out RaycastHit hit,
			float distance,
			int layerMask,
			QueryTriggerInteraction triggerInteraction
		);

		int OverlapSphereNonAlloc(
			Vector3 origin,
			float radius,
			Collider[] buffer,
			int layerMask,
			QueryTriggerInteraction triggerInteraction
		);

		int SphereCastNonAlloc(
			Vector3 origin,
			float radius,
			Vector3 direction,
			RaycastHit[] sphereCastBuffer,
			float distance,
			int defaultAndFloor,
			QueryTriggerInteraction ignore
		);

		int OverlapBoxNonAlloc(
			Vector3 center,
			Vector3 halfExtents,
			Collider[] results,
			Quaternion orientation,
			int mask,
			QueryTriggerInteraction queryTriggerInteraction
		);
	}
}