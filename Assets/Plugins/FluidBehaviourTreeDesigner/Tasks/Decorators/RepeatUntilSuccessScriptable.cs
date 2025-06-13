using System;

namespace FluidBehaviourTreeDesigner.Tasks.Decorators
{
	[Serializable]
	[Task("repeatUntilSuccess")]
	[TaskDescription("This decorator keeps calling its child until the child returns a SUCCESS value. When this happen, the decorator return a SUCCESS state.")]
	public class RepeatUntilSuccessScriptable : ADecoratorBaseScriptable
	{
		
	}
}