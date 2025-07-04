//------------------------------------------------------------------------------
// <auto-generated>
//		This code was generated by a tool (Genesis v2.3.2.0).
//
//
//		Changes to this file may cause incorrect behavior and will be lost if
//		the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using JCMG.EntitasRedux;

public sealed class VelocityEventSystem : JCMG.EntitasRedux.ReactiveSystem<GameEntity>
{

	public VelocityEventSystem(IContext<GameEntity> context) : base(context)
	{
	}

	protected override JCMG.EntitasRedux.ICollector<GameEntity> GetTrigger(JCMG.EntitasRedux.IContext<GameEntity> context)
	{
		return new SlimCollector<GameEntity>(context, GameComponentsLookup.Velocity, EventType.Added);
	}

	protected override bool Filter(GameEntity entity)
	{
		return entity.HasVelocity && entity.HasVelocityListener;
	}

	protected override void Execute(System.Collections.Generic.IEnumerable<GameEntity> entities)
	{
		foreach (var e in entities)
		{
			var component = e.Velocity;
			e.VelocityListener.Invoke(e, component.Value);
		}
	}
}
