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
	public Ecs.Common.Components.ResourcePrefabComponent ResourcePrefab { get { return (Ecs.Common.Components.ResourcePrefabComponent)GetComponent(GameComponentsLookup.ResourcePrefab); } }
	public bool HasResourcePrefab { get { return HasComponent(GameComponentsLookup.ResourcePrefab); } }

	public void AddResourcePrefab(Db.GameObjectsBase.Impl.EGameResourceType newValue)
	{
		var index = GameComponentsLookup.ResourcePrefab;
		var component = (Ecs.Common.Components.ResourcePrefabComponent)CreateComponent(index, typeof(Ecs.Common.Components.ResourcePrefabComponent));
		#if !ENTITAS_REDUX_NO_IMPL
		component.Value = newValue;
		#endif
		AddComponent(index, component);
	}

	public void ReplaceResourcePrefab(Db.GameObjectsBase.Impl.EGameResourceType newValue)
	{
		var index = GameComponentsLookup.ResourcePrefab;
		var component = (Ecs.Common.Components.ResourcePrefabComponent)CreateComponent(index, typeof(Ecs.Common.Components.ResourcePrefabComponent));
		#if !ENTITAS_REDUX_NO_IMPL
		component.Value = newValue;
		#endif
		ReplaceComponent(index, component);
	}

	public void CopyResourcePrefabTo(Ecs.Common.Components.ResourcePrefabComponent copyComponent)
	{
		var index = GameComponentsLookup.ResourcePrefab;
		var component = (Ecs.Common.Components.ResourcePrefabComponent)CreateComponent(index, typeof(Ecs.Common.Components.ResourcePrefabComponent));
		#if !ENTITAS_REDUX_NO_IMPL
		component.Value = copyComponent.Value;
		#endif
		ReplaceComponent(index, component);
	}

	public void RemoveResourcePrefab()
	{
		RemoveComponent(GameComponentsLookup.ResourcePrefab);
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
public partial class GameEntity : IResourcePrefabEntity { }

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
	static JCMG.EntitasRedux.IMatcher<GameEntity> _matcherResourcePrefab;

	public static JCMG.EntitasRedux.IMatcher<GameEntity> ResourcePrefab
	{
		get
		{
			if (_matcherResourcePrefab == null)
			{
				var matcher = (JCMG.EntitasRedux.Matcher<GameEntity>)JCMG.EntitasRedux.Matcher<GameEntity>.AllOf(GameComponentsLookup.ResourcePrefab);
				matcher.ComponentNames = GameComponentsLookup.ComponentNames;
				_matcherResourcePrefab = matcher;
			}

			return _matcherResourcePrefab;
		}
	}
}
