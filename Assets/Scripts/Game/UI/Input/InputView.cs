using SimpleUi.Abstracts;
using UniRx;
using UnityEngine.EventSystems;

namespace Game.UI.Input
{
    public class InputView : UiView, 
        IPointerDownHandler,
        IPointerUpHandler,
        IPointerMoveHandler
    {
        public ReactiveCommand<PointerEventData> OnPointerDownCommand { get; } = new();
        public ReactiveCommand<PointerEventData> OnPointerDragCommand { get; } = new();
        public ReactiveCommand<PointerEventData> OnPointerUpCommand { get; } = new();

        #region IPointerDownHandler Members

        public void OnPointerDown(PointerEventData eventData)
        {
            OnPointerDownCommand.Execute(eventData);
        }

        #endregion

        #region IPointerMoveHandler Members

        public void OnPointerMove(PointerEventData eventData)
        {
            OnPointerDragCommand.Execute(eventData);
        }

        #endregion

        #region IPointerUpHandler Members

        public void OnPointerUp(PointerEventData eventData)
        {
            OnPointerUpCommand.Execute(eventData);
        }

        #endregion
    }
}