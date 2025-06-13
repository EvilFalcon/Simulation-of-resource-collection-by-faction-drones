namespace FluidBehaviourTreeDesigner.Tasks.Decorators
{
	[Task("inverter")]
	[TaskDescription("Like the NOT operator, the inverter decorator negates the result of its child node, " +
	                 "i.e., SUCCESS state becomes FAILURE, and FAILURE becomes SUCCESS. RUNNING or ERROR states " +
	                 "are returned as is.")]
	public class InverterScriptable : ADecoratorBaseScriptable
	{
		
	}
}