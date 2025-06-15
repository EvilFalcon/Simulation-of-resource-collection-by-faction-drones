using UnityEngine;

namespace Game.Utils
{
	public class LayerMasks
	{
		private static readonly Mask PlayerMask = new Mask(Layers.Player);
		private static readonly Mask InteractableMask = new Mask(Layers.Interactable);
		private static readonly Mask GroundMask = new Mask(Layers.Ground);
		private static readonly Mask FloorMask = new Mask(Layers.Floor);
		private static readonly Mask InteractableAndAiMask = new Mask(Layers.Interactable, Layers.Ai);
		private static readonly Mask GroundAndFloorMask = new Mask(Layers.Ground, Layers.Floor);
		private static readonly Mask DefaultAndFloorMask = new Mask(Layers.Default, Layers.Floor);
		private static readonly Mask UnitMask = new Mask(Layers.Unit);
		private static readonly Mask ResourceMask = new Mask(Layers.Unit, Layers.Resources,Layers.Building);

		private static readonly Mask AttackableMask =
			new Mask(Layers.Player, Layers.Default, Layers.Ground, Layers.Floor, Layers.Interactable);

		private static readonly Mask AllMask = new Mask(Layers.Default, Layers.Player, Layers.Interactable,
			Layers.Ground, Layers.Floor, Layers.Ai);

		private static readonly Mask AllWithoutPlayerMask = new Mask(Layers.Default, Layers.Interactable,
			Layers.Ground, Layers.Floor, Layers.Ai);

		public static int Player => PlayerMask.Value;
		public static int Unit => UnitMask.Value;
		public static int Resource => ResourceMask.Value;
		public static int Interactable => InteractableMask.Value;
		public static int Ground => GroundMask.Value;
		public static int Floor => FloorMask.Value;
		public static int GroundAndFloor => GroundAndFloorMask.Value;
		public static int InteractableAndAi => InteractableAndAiMask.Value;
		public static int All => AllMask.Value;
		public static int AllWithoutPlayer => AllWithoutPlayerMask.Value;
		public static int Attackable => AttackableMask.Value;
		public static int DefaultAndFloor => DefaultAndFloorMask.Value;

		public static int HitTargets
		{
			get
			{
				var ai = 1 << Layers.AiLayer;
				var player = 1 << Layers.PlayerLayer;
				var ignoreRaycast = 1 << Layers.IgnoreRaycastLayer;
				return ~(ai | player | ignoreRaycast);
			}
		}

		#region Nested type: Mask

		private class Mask
		{
			private readonly string[] _layerNames;

			private int? _value;

			public Mask(params string[] layerNames)
			{
				_layerNames = layerNames;
			}

			public int Value
			{
				get
				{
					if (!_value.HasValue)
						_value = LayerMask.GetMask(_layerNames);
					return _value.Value;
				}
			}
		}

		#endregion
	}
}