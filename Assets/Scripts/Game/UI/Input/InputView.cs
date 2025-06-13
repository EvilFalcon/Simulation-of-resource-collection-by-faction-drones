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
        
        public void OnPointerDown(PointerEventData eventData)
        {
            OnPointerDownCommand.Execute(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            OnPointerUpCommand.Execute(eventData);
        }

        public void OnPointerMove(PointerEventData eventData)
        {
            OnPointerDragCommand.Execute(eventData);
        }
    }
}