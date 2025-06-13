using System.Collections.Generic;

namespace Ecs.Utils
{
	public interface IStaticEntityOwner
	{
		void CreateStaticEntities(GameEntity owner, GameContext game, List<GameEntity> entities);

		void ConfigureStatic(IEnumerable<GameEntity> entities, GameContext game);
	}
}