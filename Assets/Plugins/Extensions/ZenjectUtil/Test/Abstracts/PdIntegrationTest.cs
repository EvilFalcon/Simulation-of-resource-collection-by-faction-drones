using JCMG.EntitasRedux.VisualDebugging;
//using NUnit.Framework;
using UnityEngine;
using Zenject;
using ZenjectUtil.Test.Extensions;

namespace Tests.Abstracts
{
	public abstract class PdIntegrationTest
	{

		//[SetUp]
		public virtual void SetUp()
		{
			ResourceSubstitute.Clear();
			SubstituteResources();
		}

		protected abstract void SubstituteResources();

		//[TearDown]
		public virtual void TearDown()
		{
			ResourceSubstitute.Clear();
			
#if (!ENTITAS_DISABLE_VISUAL_DEBUGGING && UNITY_EDITOR)
			var observerBehaviours = Object.FindObjectsOfType<ContextObserverBehaviour>();
			foreach (var ob in observerBehaviours)
			{
				Object.Destroy(ob.gameObject);
			}
#endif
			
			Object.Destroy(ProjectContext.Instance.gameObject);
		}
		
	}
}