using UnityEngine;

namespace Core.Impl.Utils
{
	public static class AnimationKeys
	{
		public static readonly int X = Animator.StringToHash("X");
		public static readonly int Y = Animator.StringToHash("Y");
		public static readonly int PushSpeed = Animator.StringToHash("PushSpeed");
		public static readonly int Grounded = Animator.StringToHash("Grounded");
		public static readonly int Jump = Animator.StringToHash("Jump");
		public static readonly int VerticalVelocity = Animator.StringToHash("VerticalVelocity");
		public static readonly int IsMoving = Animator.StringToHash("IsMoving");
		public static readonly int Sneak = Animator.StringToHash("Sneak");
		public static readonly int Attack = Animator.StringToHash("Attack");
		public static readonly int Aiming = Animator.StringToHash("Aiming");
		public static readonly int UseTool = Animator.StringToHash("UseTool");
		public static readonly int IsSwim = Animator.StringToHash("IsSwim");
		public static readonly int SwimSpeed = Animator.StringToHash("SwimSpeed");
		public static readonly int Dead = Animator.StringToHash("Dead");
		public static readonly int ClimbSpeed = Animator.StringToHash("ClimbSpeed");
		public static readonly int Climbing = Animator.StringToHash("Climbing");
		public static readonly int ClimbingOn = Animator.StringToHash("ClimbingOn");
		public static readonly int Knocked = Animator.StringToHash("Knocked");
		public static readonly int FirstPerson = Animator.StringToHash("FirstPerson");
	}
}