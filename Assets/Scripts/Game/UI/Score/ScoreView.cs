using SimpleUi.Abstracts;
using TMPro;
using UnityEngine;

namespace Game.UI.Score
{
    public class ScoreView : UiView
    {
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private TextMeshProUGUI _bestScoreText;

        public void UpdateScore(int newScore)
        {
            _scoreText.text = $"Score: {newScore}";
        }
        
        public void UpdateBestScore(int newBestScore)
        {
            _bestScoreText.text = $"Best score: {newBestScore}";
        }
    }
}