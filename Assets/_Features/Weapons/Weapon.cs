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
        [BoxGroup("Stats"), SerializeField] protected float _fireRate;
        [BoxGroup("Stats"), SerializeField] protected float _damage;
        [BoxGroup("Stats"), SerializeField] protected float _recoil = 5f;
        [BoxGroup("Stats"), SerializeField] protected float _recoilTime = 0.1f;
        
        [BoxGroup("Other"), SerializeField] private Vector3 _inHandOffset;

        protected int _currentAmmo;
        protected float _currentFireRate;
        
        public BoxCollider Collider => _collider;
        public Sprite Icon => _icon;
        public Vector3 InHandOffset => _inHandOffset;
        public int CurrentAmmo => _currentAmmo;
        public float Recoil => _recoil;
        public float RecoilTime => _recoilTime;
        public float NormalizedFireRate => _currentFireRate / _fireRate;
        private bool CanShoot => _currentAmmo > 0 && _currentFireRate == 0;

        private event Action PlayPlayerShootAnim;
        
        private void Awake()
        {
            _currentAmmo = _maxAmmo;
        }

        public void Equip(Action p_playPlayerShootAnim)
        {
            PlayPlayerShootAnim = p_playPlayerShootAnim;
        }

        public void UnEquip()
        {
            PlayPlayerShootAnim = null;
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
        }

        public void HoldTrigger()
        {
            if (!CanShoot) return;
            
            OnHoldTrigger();
        }

        protected virtual void CreateProjectiles()
        {
            WeaponProjectile projectile = Instantiate(_projectilePrefab, _barrel.position, _barrel.rotation);
            projectile.Setup(_damage);
        }
        
        protected void Shoot()
        {
            CreateProjectiles();

            Instantiate(_smokeParticle, _barrel.position, _barrel.rotation);
            
            _currentAmmo--;
            
            _currentFireRate = _fireRate;
            
            PlayPlayerShootAnim?.Invoke();
        }

        protected virtual void OnPressTrigger() => Shoot();
        protected virtual void OnHoldTrigger() => Shoot();
    }
}