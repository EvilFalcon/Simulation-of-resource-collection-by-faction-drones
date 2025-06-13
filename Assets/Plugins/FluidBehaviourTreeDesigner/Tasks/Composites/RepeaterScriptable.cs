using System;

namespace FluidBehaviourTreeDesigner.Tasks.Composites
{
	[Serializable]
	[Task("repeater")]
	[TaskDescription("The Repeater node ticks its children sequentially from left to right, until one of them returns " +
	                 "FAILURE, RUNNING or ERROR, at which point the Sequence returns that state. If all children return " +
	                 "the success state, the sequence also returns SUCCESS. After status CONTINUE an entire node will be repeated again.")]
	public class RepeaterScriptable : ACompositeScriptable
	{
		
	}
}