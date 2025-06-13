using System;

namespace FluidBehaviourTreeDesigner.Tasks.Composites
{
	[Serializable]
	[Task("selector repeater")]
	[TaskDescription(
		"The Selector node ticks its children sequentially from left to right, until one of them returns " +
		"SUCCESS, RUNNING or ERROR, at which point it returns that state. If all children return the failure" +
		" state, the priority also returns FAILURE. After status CONTINUE an entire node will be repeated again.")]
	public class SelectorRepeaterScriptable : ACompositeScriptable
	{
	}
}