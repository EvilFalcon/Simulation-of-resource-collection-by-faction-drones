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
	public Ecs.Game.Components.Fraction.FractionResourcesComponent FractionResources { get { return (Ecs.Game.Components.Fraction.FractionResourcesComponent)GetComponent(GameComponentsLookup.FractionResources); } }
	public bool HasFractionResources { get { return HasComponent(GameComponentsLookup.FractionResources); } }

	public void AddFractionResources(Ecs.Game.Components.Fraction.FractionResources newValue)
	{
		var index = GameComponentsLookup.FractionResources;
		var component = (Ecs.Game.Components.Fraction.FractionResourcesComponent)CreateComponent(index, typeof(Ecs.Game.Components.Fraction.FractionResourcesComponent));
		#if !ENTITAS_REDUX_NO_IMPL
		component.Value = newValue;
		#endif
		AddComponent(index, component);
	}

	public void ReplaceFractionResources(Ecs.Game.Components.Fraction.FractionResources newValue)
	{
		var index = GameComponentsLookup.FractionResources;
		var component = (Ecs.Game.Components.Fraction.FractionResourcesComponent)CreateComponent(index, typeof(Ecs.Game.Components.Fraction.FractionResourcesComponent));
		#if !ENTITAS_REDUX_NO_IMPL
		component.Value = newValue;
		#endif
		ReplaceComponent(index, component);
	}

	public void CopyFractionResourcesTo(Ecs.Game.Components.Fraction.FractionResourcesComponent copyComponent)
	{
		var index = GameComponentsLookup.FractionResources;
		var component = (Ecs.Game.Components.Fraction.FractionResourcesComponent)CreateComponent(index, typeof(Ecs.Game.Components.Fraction.FractionResourcesComponent));
		#if !ENTITAS_REDUX_NO_IMPL
		component.Value = copyComponent.Value;
		#endif
		ReplaceComponent(index, component);
	}

	public void RemoveFractionResources()
	{
		RemoveComponent(GameComponentsLookup.FractionResources);
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
public partial class GameEntity : IFractionResourcesEntity { }

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
	static JCMG.EntitasRedux.IMatcher<GameEntity> _matcherFractionResources;

	public static JCMG.EntitasRedux.IMatcher<GameEntity> FractionResources
	{
		get
		{
			if (_matcherFractionResources == null)
			{
				var matcher = (JCMG.EntitasRedux.Matcher<GameEntity>)JCMG.EntitasRedux.Matcher<GameEntity>.AllOf(GameComponentsLookup.FractionResources);
				matcher.ComponentNames = GameComponentsLookup.ComponentNames;
				_matcherFractionResources = matcher;
			}

			return _matcherFractionResources;
		}
	}
}
