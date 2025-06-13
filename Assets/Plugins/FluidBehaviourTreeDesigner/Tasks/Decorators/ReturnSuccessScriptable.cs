using System;

namespace FluidBehaviourTreeDesigner.Tasks.Decorators
{
	[Serializable]
	[Task("return success")]
	[TaskDescription("Succeeder always returns a SUCCESS, no matter what its child returns.")]
	public class ReturnSuccessScriptable : ADecoratorBaseScriptable
	{
	}
}