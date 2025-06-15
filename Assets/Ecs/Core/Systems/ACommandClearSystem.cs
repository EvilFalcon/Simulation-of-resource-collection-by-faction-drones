using JCMG.EntitasRedux;
using JCMG.EntitasRedux.Commands;

namespace Ecs.Core.Systems
{
    public abstract class ACommandClearSystem<TCommand> : IUpdateSystem where TCommand : struct
    {
        private readonly ICommandBuffer _commandBuffer;

        protected ACommandClearSystem(ICommandBuffer commandBuffer)
        {
            _commandBuffer = commandBuffer;
        }

        #region IUpdateSystem Members

        public void Update()
        {
            _commandBuffer.Clear<TCommand>();
        }

        #endregion
    }
}