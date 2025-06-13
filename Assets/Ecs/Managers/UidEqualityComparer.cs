using System.Collections.Generic;

namespace Ecs.Managers
{
	public class UidEqualityComparer : IEqualityComparer<Uid>
	{
		public static readonly UidEqualityComparer Instance = new UidEqualityComparer();

		public bool Equals(Uid x, Uid y) => x == y;

		public int GetHashCode(Uid obj) => obj.GetHashCode();
	}
}