using UnityEngine;

namespace Ecs.Utils
{
	public readonly struct AttackInfo
	{
		public readonly Vector3 ShotDirection;
		public readonly float AttackDamage;
		public readonly InteractedInfo? InteractedInfo;

		public AttackInfo(Vector3 shotDirection, float attackDamage, InteractedInfo? interactedInfo)
		{
			ShotDirection = shotDirection;
			AttackDamage = attackDamage;
			InteractedInfo = interactedInfo;
		}
	}
}