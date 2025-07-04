//------------------------------------------------------------------------------
// <auto-generated>
//		This code was generated by a tool (Genesis v2.3.2.0).
//
//
//		Changes to this file may cause incorrect behavior and will be lost if
//		the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity
{
	public RayListenerComponent RayListener { get { return (RayListenerComponent)GetComponent(GameComponentsLookup.RayListener); } }
	public bool HasRayListener { get { return HasComponent(GameComponentsLookup.RayListener); } }

	public void AddRayListener()
	{
		var index = GameComponentsLookup.RayListener;
		var component = (RayListenerComponent)CreateComponent(index, typeof(RayListenerComponent));
		AddComponent(index, component);
	}

	public void ReplaceRayListener()
	{
		ReplaceComponent(GameComponentsLookup.RayListener, RayListener);
	}

	public void RemoveRayListener()
	{
		RemoveComponent(GameComponentsLookup.RayListener);
	}
}

//------------------------------------------------------------------------------
// <auto-generated>
//		This code was generated by a tool (Genesis v2.3.2.0).
//
//
//		Changes to this file may cause incorrect behavior and will be lost if
//		the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher
{
	static JCMG.EntitasRedux.IMatcher<GameEntity> _matcherRayListener;

	public static JCMG.EntitasRedux.IMatcher<GameEntity> RayListener
	{
		get
		{
			if (_matcherRayListener == null)
			{
				var matcher = (JCMG.EntitasRedux.Matcher<GameEntity>)JCMG.EntitasRedux.Matcher<GameEntity>.AllOf(GameComponentsLookup.RayListener);
				matcher.ComponentNames = GameComponentsLookup.ComponentNames;
				_matcherRayListener = matcher;
			}

			return _matcherRayListener;
		}
	}
}

//------------------------------------------------------------------------------
// <auto-generated>
//		This code was generated by a tool (Genesis v2.3.2.0).
//
//
//		Changes to this file may cause incorrect behavior and will be lost if
//		the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity
{
	public System.IDisposable SubscribeRay(OnGameRay value, bool invokeOnSubscribe = true)
	{
		var componentListener = HasRayListener
			? RayListener
			: (RayListenerComponent)CreateComponent(GameComponentsLookup.RayListener, typeof(RayListenerComponent));
		componentListener.Delegate += value;
		ReplaceComponent(GameComponentsLookup.RayListener, componentListener);
		if(invokeOnSubscribe && HasComponent(GameComponentsLookup.Ray))
		{
			value(this);
		}

		return new JCMG.EntitasRedux.Events.EventDisposable<OnGameRay>(CreationIndex, value, UnsubscribeRay);
	}

	private void UnsubscribeRay(int creationIndex, OnGameRay value)
	{
		if(!IsEnabled || CreationIndex != creationIndex)
			return;

		var index = GameComponentsLookup.RayListener;
		var component = RayListener;
		component.Delegate -= value;
		if (RayListener.IsEmpty)
		{
			RemoveComponent(index);
		}
		else
		{
			ReplaceComponent(index, component);
		}
	}
}
