using SimpleUi.Abstracts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.DebugView
{
    public class DebugView : UiView
    {
        [field: SerializeField] public TextMeshProUGUI CountUnitsText;
        [field: SerializeField] public Slider UnitsCountSlider;

        [field: SerializeField] public TextMeshProUGUI UnitsSpeedText;
        [field: SerializeField] public Slider UnitsSpeedSlider;

        [field: SerializeField] public InputField SpawnDelay;

        [field: SerializeField] public TextMeshProUGUI NavMeshAgentTogle;
        [field: SerializeField] public Button NavMeshAgentButton;
    }
}