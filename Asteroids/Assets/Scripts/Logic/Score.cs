using Assets.Scripts.View;

namespace Assets.Scripts.Logic
{
    public class Score
    {
        public int GetScore => _points;

        private readonly ScoreView _scoreView;

        private int _points;

        public Score(ScoreView scoreView)
        {
            _scoreView = scoreView;
        }

        public void AddPoint(int point)
        {
            _points += point;
            _scoreView.SetScore(_points);
        }
    }
}
