
using Assets.Scripts.View;

namespace Assets.Scripts.Logic
{
    public class HealthPointsHandler
    {
        private const int MIN_HEALTH_POINTS = 0;

        private readonly HealthPointView _healthPointView;
        private readonly int _maxHealthPoints;

        private int _healthPoints;

        public HealthPointsHandler(int startHealthPoints, HealthPointView healthPointView)
        {
            _healthPointView = healthPointView;
            _maxHealthPoints = _healthPointView.GetMaxHp();
            CheckStartHealthPoints(startHealthPoints);
        }
        public void AddHP()
        {
            if (_healthPoints < _maxHealthPoints)
            {
                _healthPoints++;
                _healthPointView.SetSprite(_healthPoints);
            }

        }
        public int DecreaseHP()
        {
            if (_healthPoints > MIN_HEALTH_POINTS)
            {
                _healthPoints--;
                _healthPointView.SetSprite(_healthPoints);
            }
            return _healthPoints;
        }
        private void CheckStartHealthPoints(int startHealthPoints)
        {
            if (startHealthPoints > _maxHealthPoints)
            {
                _healthPoints = _maxHealthPoints;
            }
            if (startHealthPoints <= MIN_HEALTH_POINTS)
            {
                _healthPoints = MIN_HEALTH_POINTS + 1;
            }
            _healthPoints = startHealthPoints;
            _healthPointView.SetSprite(_healthPoints);
        }
    }
}
