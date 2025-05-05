using UnityEngine;

public interface IProjectile 
{
    public virtual void Launch(Vector2 direction, float speed) { }
}
