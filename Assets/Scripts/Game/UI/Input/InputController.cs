using Generated.Commands;
using JCMG.EntitasRedux.Commands;
using PdUtils.Interfaces;
using SimpleUi.Abstracts;
using UniRx;
using UnityEngine.EventSystems;

namespace Game.UI.Input
{
    public class InputController : UiController<InputView>, IUiInitializable
    {
        private readonly ICommandBuffer _commandBuffer;

        public InputController(ICommandBuffer commandBuffer)
        {
            _commandBuffer = commandBuffer;
        }

        #region IUiInitializable Members

        public void Initialize()
        {
            View.OnPointerDownCommand.Subscribe(OnBeginDrag).AddTo(View);
            View.OnPointerDragCommand.Subscribe(OnDrag).AddTo(View);
            View.OnPointerUpCommand.Subscribe(OnEndDragUp).AddTo(View);
        }

        #endregion

        private void OnBeginDrag(PointerEventData eventData)
        {
            _commandBuffer.PointerDown(eventData.position);
        }

        private void OnDrag(PointerEventData eventData)
        {
            _commandBuffer.PointerMove(eventData.position);
        }

        private void OnEndDragUp(PointerEventData eventData)
        {
            _commandBuffer.PointerUp();
        }
    }
}