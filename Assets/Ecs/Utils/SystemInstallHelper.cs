using System;
using Ecs.Utils.SystemInstallProcessors;
using PdUtils.ProcessorChain;
using PdUtils.ProcessorChain.Impl;
using Zenject;

namespace Ecs.Utils
{
	public class SystemInstallHelper
	{
		private static readonly IProcessorChain<Request, Response> Chain = CreateChain();

		public static void Install<TSystem>(DiContainer container)
		{
			var request = new Request
			{
				SystemType = typeof(TSystem),
			};

			var response = new Response
			{
				Bind = true
			};

			Chain.DoProcess(request, ref response);

			if (response.Bind) container.BindInterfacesAndSelfTo<TSystem>().AsSingle();

			Chain.Reset();
		}


		private static IProcessorChain<Request, Response> CreateChain()
		{
			var processorChain = new ProcessorChain<Request, Response>();
			processorChain.AddProcessor(new RegularModeProcessor());
			return processorChain;
		}
	}


	public struct Request : IRequest
	{
		public Type SystemType;
	}

	public struct Response : IResponse
	{
		public bool Bind;
	}
}