using System;
using Zenject;

namespace Ecs.Core.Interfaces
{
	public interface IBootstrap : IInitializable, IDisposable
	{
		void Pause(bool isPaused);
		void Reset();
	}
}