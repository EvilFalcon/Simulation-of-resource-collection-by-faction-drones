using JCMG.EntitasRedux;

namespace Ecs.Core.Interfaces
{
	public interface ILateFixedSystem : ISystem
	{
		void LateFixed();
	}
}