using System;
using NaughtyAttributes;
using UnityEngine;

namespace Kosciach.StoreWars.Weapons
{
    using Projectiles;
    
    [RequireComponent(typeof(BoxCollider))]
    public abstract class Weapon : MonoBehaviour
    {
        [BoxGroup("References"), SerializeField] private BoxCollider _collider;
        [BoxGroup("References"), SerializeField] protected Transform _barrel;
        [BoxGroup("References"), SerializeField] protected WeaponProjectile _projectilePrefab;
        [BoxGroup("References"), SerializeField] protected ParticleSystem _smokeParticle;
        [BoxGroup("References"), SerializeField] private Sprite _icon;
        
        [BoxGroup("Stats"), SerializeField] private int _maxAmmo;
        [BoxGroup("Stats"), SerializeField] private float _fireRate;
        [BoxGroup("Stats"), SerializeField] protected float _damage;
        
        [BoxGroup("Other"), SerializeField] private Vector3 _inHandOffset;

        protected int _currentAmmo;
        private float _currentFireRate;
        
        public BoxCollider Collider => _collider;
        public Sprite Icon => _icon;
        public Vector3 InHandOffset => _inHandOffset;
        public int CurrentAmmo => _currentAmmo;
        private bool CanShoot => _currentAmmo > 0 && _currentFireRate == 0;
        
        
        private void Awake()
        {
            _currentAmmo = _maxAmmo;
        }

        public void UpdateWhenHeld(Quaternion p_playerRotation)
        {
            _currentFireRate = Mathf.Max(0, _currentFireRate - Time.deltaTime);
            _barrel.rotation = p_playerRotation;
        }

        public void PressTrigger()
        {
            if (!CanShoot) return;

            OnPressTrigger();

            _currentFireRate = _fireRate;
        }

        public void HoldTrigger()
        {
            if (!CanShoot) return;

            OnHoldTrigger();
            
            _currentFireRate = _fireRate;
        }

        protected virtual void Shoot()
        {
            WeaponProjectile projectile = Instantiate(_projectilePrefab, _barrel.position, _barrel.rotation);
            projectile.Setup(_damage);

            Instantiate(_smokeParticle, _barrel.position, _barrel.rotation);
            
            _currentAmmo--;
        }

        protected virtual void OnPressTrigger() => Shoot();
        protected virtual void OnHoldTrigger() => Shoot();
    }
}