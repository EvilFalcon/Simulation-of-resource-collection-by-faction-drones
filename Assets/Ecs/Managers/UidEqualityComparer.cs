using System.Collections.Generic;

namespace Ecs.Managers
{
	public class UidEqualityComparer : IEqualityComparer<Uid>
	{
		public static readonly UidEqualityComparer Instance = new UidEqualityComparer();

		#region IEqualityComparer<Uid> Members

		public bool Equals(Uid x, Uid y) => x == y;

		public int GetHashCode(Uid obj) => obj.GetHashCode();

		#endregion
	}
}