using SimpleUi.Abstracts;
using TMPro;
using UnityEngine;

namespace Game.UI.Score
{
    public class ResourcesView : UiView
    {
        [SerializeField] private TextMeshProUGUI _crystalComputerResource;

        [SerializeField] private TextMeshProUGUI _crystalPlayerResource;

        [Header("Computer Resources")] [SerializeField]
        private TextMeshProUGUI _mithrilComputerResource;

        [Header("Player Resources")] [SerializeField]
        private TextMeshProUGUI _mithrilPlayerResource;

        public void UpdatePlayerMithrilResource(int mithril)
        {
            _mithrilPlayerResource.text = $"Mithril: {mithril}";
        }

        public void UpdatePlayerCrystalResource(int crystal)
        {
            _crystalPlayerResource.text = $"Crystal: {crystal}";
        }

        public void UpdateComputerMithrilResource(int mithril)
        {
            _mithrilComputerResource.text = $"Mithril: {mithril}";
        }

        public void UpdateComputerCrystalResource(int crystal)
        {
            _crystalComputerResource.text = $"Crystal: {crystal}";
        }
    }
}