using UnityEngine;

public class BouncedBullet : Bullet
{
    private Vector2 _direction;
    private float _speed;

    private float _leftBorder;
    private float _rightBorder;
    private float _topBorder;
    private float _bottomBorder;

    public void Initialize(float left, float right, float top, float bottom)
    {
        _leftBorder = left;
        _rightBorder = right;
        _topBorder = top;
        _bottomBorder = bottom;
    }

    public override void Launch(Vector2 direction, float speed)
    {
        CurrentCollisionCount = CollisionCount;
        _speed = speed;
        _direction = direction.normalized;
    }

    private void Update()
    {
        transform.position += (Vector3)(_direction * _speed * Time.deltaTime);

        VerifyPositionAndReflect();
    }

    private void VerifyPositionAndReflect()
    {
        Vector2 normal = Vector2.zero;
        bool collided = false;

        Vector2 position = transform.position;

        if (position.x < _leftBorder)
        {
            normal = Vector2.right;
            position.x = _leftBorder;
            collided = true;
        }
        else if (position.x > _rightBorder)
        {
            normal = Vector2.left;
            position.x = _rightBorder;
            collided = true;
        }

        if (position.y > _topBorder)
        {
            normal = Vector2.down;
            position.y = _topBorder;
            collided = true;
        }
        else if (position.y < _bottomBorder)
        {
            normal = Vector2.up;
            position.y = _bottomBorder;
            collided = true;
        }

        if (collided)
        {
            if (CurrentCollisionCount == 0)
                ReportAboutCollision(this);
            else
                CurrentCollisionCount--;

            transform.position = position;

            _direction = Vector2.Reflect(_direction, normal).normalized;

            SetRotation(_direction);
        }
    }

    private void SetRotation(Vector2 direction)
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
