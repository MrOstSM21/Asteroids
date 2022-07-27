using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.View
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private Text _score;

        private string _startScore = "Your score: 0";

        private void Start()
        {
            _score.text = _startScore;
        }
        public void SetScore(int score) => _score.text = $"Your score: {score}";
    }
}
