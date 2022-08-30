using Assets.Scripts.View;

namespace Assets.Scripts.Logic
{
    public class Score
    {
        public int GetScore => _points;
        public int GetBestScore => _bestScore;

        private readonly ScoreView _scoreView;

        private int _points;
        private int _bestScore;

        public Score(ScoreView scoreView)
        {
            _scoreView = scoreView;
        }

        public void AddPoint(int point)
        {
            _points += point;
            _scoreView.SetScore(_points);
        }
        public void SetBestScore(int point) => _bestScore = point;
        public int CheckBestScore()
        {
            if (_bestScore<_points)
            {
                _bestScore = _points;
            }
            return _bestScore;
        }

    }
}
