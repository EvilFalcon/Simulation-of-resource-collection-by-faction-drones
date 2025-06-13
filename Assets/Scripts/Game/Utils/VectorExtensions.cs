using UnityEngine;

namespace Game.Utils
{
	public static class VectorExtensions
	{
		public static Vector3Int ToVector3Int(this Vector3 vector)
			=> new(
				Mathf.RoundToInt(vector.x),
				Mathf.RoundToInt(vector.y),
				Mathf.RoundToInt(vector.z)
			);

		public static Vector3Int To3D(this Vector2Int vector) => new(vector.x, 0, vector.y);

		public static Vector2 To2D(this Vector3 vector) => new(vector.x, vector.z);

		public static Vector3 To3D(this Vector2 vector) => new(vector.x, 0, vector.y);

		public static Vector3 NoY(this Vector3 vector) => new(vector.x, 0, vector.z);
	}
}