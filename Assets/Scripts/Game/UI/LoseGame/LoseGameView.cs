using SimpleUi.Abstracts;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.LoseGame
{
    public class LoseGameView : UiView
    {
        [field: SerializeField] public Button RestartButton { get; private set; }
    }
}