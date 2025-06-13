using System;

namespace FluidBehaviourTreeDesigner.Tasks.Decorators
{
	[Serializable]
	[Task("repeatUntilFailure")]
	[TaskDescription("This decorator keeps calling its child until the child returns a FAILURE value. When this happen, the decorator return a FAILURE state.")]
	public class RepeatUntilFailureScriptable : ADecoratorBaseScriptable
	{
		
	}
}