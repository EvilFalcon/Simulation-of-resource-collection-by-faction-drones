using PdUtils;

namespace Ecs.Utils
{
	public interface ILinkedEntityRepository : IRepository<int, GameEntity>
	{
		bool TryGet(int id, out GameEntity entity);
	}
}