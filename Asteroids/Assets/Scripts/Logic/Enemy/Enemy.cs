using Assets.Scripts.Logic;
using Assets.Scripts.View;
using UnityEngine;

public abstract class Enemy
{
    protected readonly Settings _settings;
    protected readonly Score _score;
    protected readonly UpdateHandler _updateHandler;

    protected VisibilityHandler _visibilityHandler;
    protected IMovement _movement;
    protected Enemy _enemy;
    protected float _speed;
    protected Vector3 _direction;
    protected EnemyView _enemyView;
    protected int _enemyPoints;
    protected Enemy(Settings settings, Score score, UpdateHandler updateHandler)
    {
        _settings = settings;
        _score = score;
        _updateHandler = updateHandler;
    }

    public void LeftTheZone()
    {
        Unsubscribe();
        _enemyView.Destroy();
    }

    public void Move()
    {
        _visibilityHandler.CheckVisibilityEnemy(_enemy, _settings.GetEndZoneDistanse);
        _movement.Move(_speed, _direction);
    }
    public abstract void Subscribe();

    public abstract void Unsubscribe();

    protected void GetDamage()
    {
        _score.AddPoint(_enemyPoints);
        Unsubscribe();
    }

}
