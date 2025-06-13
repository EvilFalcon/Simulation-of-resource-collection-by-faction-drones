using UnityEngine;

namespace Ecs.Core.Interfaces
{
	public interface IPrefabsProvider
	{
		GameObject Get(string name);
	}
}