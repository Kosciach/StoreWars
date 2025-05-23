using UnityEngine;

namespace Kosciach.StoreWars.Projectiles
{
    public interface IDamageable
    {
        void TakeDamage(float p_damage);
        void Push(Vector3 p_direction);
    }
}
