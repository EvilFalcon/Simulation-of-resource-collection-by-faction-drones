using System;

namespace FluidBehaviourTreeDesigner.Tasks.Composites
{
	[Serializable]
	[Task("parallel")]
	[TaskDescription("The parallel node ticks all children sequentially from left to right, regardless of their return " +
	                 "states. It returns SUCCESS if the number of succeeding children is larger than a local constant S, " +
	                 "FAILURE if the number of failing children is larger than a local constant F or RUNNING otherwise.")]
	public class ParallelScriptable : ACompositeScriptable
	{
		
	}
}