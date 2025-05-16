using System;
using Kosciach.StoreWars.Weapons.Projectiles;
using NaughtyAttributes;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Kosciach.StoreWars.Weapons
{
    public class Shotgun : Weapon
    {
        [BoxGroup("Stats"), SerializeField] private int _projectilesPerShot;
        [BoxGroup("Stats"), SerializeField] private Vector2 _bulletSpread;

        protected override void Shoot()
        {
            for (int i = 0; i < _projectilesPerShot; i++)
            {
                float xBulletSpread = Random.Range(-_bulletSpread.x, _bulletSpread.x);
                float yBulletSpread = Random.Range(-_bulletSpread.y, _bulletSpread.y);
                Quaternion rotation = _barrel.rotation * Quaternion.Euler(yBulletSpread, xBulletSpread, 0);
                
                WeaponProjectile projectile = Instantiate(_projectilePrefab, _barrel.position, rotation);
                projectile.Setup(_damage);
            }
            
            _currentAmmo--;
        }
    }
}