using System;

namespace FluidBehaviourTreeDesigner.Tasks.Composites
{
	[Serializable]
	[Task("selectorRandom")]
	[TaskDescription("The Selector node ticks its children randomly with a shuffle algorithm, until one of them returns " +
	                 "SUCCESS, RUNNING or ERROR, at which point it returns that state. If all children return the failure" +
	                 " state, the priority also returns FAILURE.")]
	public class SelectorRandomScriptable : ACompositeScriptable
	{
		
	}
}