using System;
using UnityEngine;

namespace Ecs.Managers
{
	[Serializable]
	public struct Uid : IEquatable<Uid>
	{
		public static readonly Uid Empty = new Uid();

		[SerializeField] private uint value;

		private Uid(uint value)
		{
			this.value = value;
		}

		public bool Equals(Uid other) => value == other.value;

		public override bool Equals(object obj) => obj is Uid uid && Equals(uid);

		public override int GetHashCode() => (int)value;

		public static explicit operator Uid(uint value) => new Uid(value);

		public static explicit operator uint(Uid uid) => uid.value;

		public static bool operator ==(Uid a, Uid b) => a.value == b.value;

		public static bool operator !=(Uid a, Uid b) => a.value != b.value;

		public override string ToString() => $"Uid #{value}";

		public static Uid Parse(string value)
		{
			var tmp = value.Remove(0, 5);
			return (Uid)uint.Parse(tmp);
		}
	}
}