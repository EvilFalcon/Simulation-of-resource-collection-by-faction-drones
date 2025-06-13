using System;

namespace FluidBehaviourTreeDesigner.Tasks.Decorators
{
	[Serializable]
	[Task("repeatForever")]
	[TaskDescription("Repeat forever decorator sends the tick signal to its child every time that its child returns a SUCCESS or FAILURE.")]
	public class RepeatForeverScriptable : ACompositeScriptable
	{
		
	}
}