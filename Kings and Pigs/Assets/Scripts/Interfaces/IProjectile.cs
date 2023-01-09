using UnityEngine;

public interface IProjectile
{
    void Throw(Vector2 targetPosition, Transform thrower);
}
