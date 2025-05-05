using System;
using UnityEngine;

public abstract class Bullet : MonoBehaviour, IProjectile
{
    [SerializeField] protected int CollisionCount = 2;

    protected int CurrentCollisionCount;

    public event Action<Bullet> CollisionHappened;

    public virtual void Launch(Vector2 direction, float speed) {}

    protected virtual void ReportAboutCollision(Bullet bullet)
    { 
        CollisionHappened?.Invoke(bullet);
    }
}
