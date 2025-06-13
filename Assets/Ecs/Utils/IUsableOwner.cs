using System.Collections.Generic;

namespace Ecs.Utils
{
	public interface IUsableOwner
	{
		List<GameEntity> CreateUsableEntities(GameEntity owner, GameContext context);
		void LinkUsables(List<GameEntity> entities);
	}
}