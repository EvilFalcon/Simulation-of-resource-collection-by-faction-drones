using System;

namespace FluidBehaviourTreeDesigner.Tasks.Composites
{
	[Serializable]
	[Task("sequence")]
	[TaskDescription("The Sequence node ticks its children sequentially from left to right, until one of them returns " +
	                 "FAILURE, RUNNING or ERROR, at which point the Sequence returns that state. If all children return " +
	                 "the success state, the sequence also returns SUCCESS.")]
	public class SequenceScriptable : ACompositeScriptable
	{
	}
}