using JCMG.EntitasRedux.Core.View;
using UnityEngine;

namespace Ecs.Views.Linkable
{
	public interface IObjectLinkable : ILinkable
	{
		Transform Transform { get; }
	}
}