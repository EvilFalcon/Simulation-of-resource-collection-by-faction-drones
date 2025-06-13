using System;
using System.Collections.Generic;

namespace FluidBehaviourTreeDesigner.Tasks
{
	[Serializable]
	public abstract class AActionScriptable : ATaskScriptable
	{
		// Child connections
		public override void ConnectChild(ATaskScriptable child)
		{
		}

		public override void DisconnectChild(ATaskScriptable child)
		{
		}

		public override List<ATaskScriptable> Children => new List<ATaskScriptable>();

		public override int ChildCount => 0;

		public override bool CanConnectChild => false;

		public override bool ContainsChild(ATaskScriptable child) => false;
	}
}