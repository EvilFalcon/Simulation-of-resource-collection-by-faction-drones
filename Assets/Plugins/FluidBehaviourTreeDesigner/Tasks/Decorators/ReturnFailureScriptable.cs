using System;

namespace FluidBehaviourTreeDesigner.Tasks.Decorators
{
	[Serializable]
	[Task("return failure")]
	[TaskDescription("Succeeder always returns a FAILURE, no matter what its child returns.")]
	public class ReturnFailureScriptable : ADecoratorBaseScriptable
	{
	}
}