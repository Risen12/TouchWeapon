using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PhysicBullet : Bullet
{
    [SerializeField] private LayerMask _wallLayerMask;

    private float _powerOfShot;
    private Rigidbody2D _rigidbody2D;

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _powerOfShot = 0f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if ((_wallLayerMask.value & (1 << collision.gameObject.layer)) != 0)
        {
            ReportAboutCollision(this);
        }
    }

    public void SetPower(float power) => _powerOfShot = power;

    public override void Launch(Vector2 direction, float speed)
    {
        CurrentCollisionCount = CollisionCount;
        _rigidbody2D.AddForce(direction * speed * _powerOfShot);
    }
}
