using UnityEngine;
using TMPro;

namespace Assets.Scripts.View
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _score;
        [SerializeField] private string _scoreName;

        private string _startScore; 

        private void Start()
        {
            _startScore = $"{_scoreName}: 0";
            _score.text = _startScore;
        }
        public void SetScore(int score) => _score.text = $"{_scoreName}: {score}";
    }
}
