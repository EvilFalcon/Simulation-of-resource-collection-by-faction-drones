using PdUtils.ProcessorChain;

namespace Ecs.Utils.SystemInstallProcessors
{
	public class RegularModeProcessor : IProcessor<Request, Response>
	{
		public void DoProcess(Request request, ref Response response, IProcessorChain<Request, Response> processorChain)
		{
			processorChain.DoProcess(request, ref response);
		}
	}
}