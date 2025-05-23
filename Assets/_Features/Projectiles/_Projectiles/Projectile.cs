using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;

namespace Kosciach.StoreWars.Projectiles
{
    public abstract class WeaponProjectile : MonoBehaviour
    {
        private float _damage;
        private float _speed;
        
        [BoxGroup("References"), SerializeField] private ParticleSystem _hitParticle;

        [BoxGroup("Stats"), SerializeField] private float _baseSpeed = 30;
        [BoxGroup("Stats"), SerializeField] private float _rayRange = 1;
        [BoxGroup("Stats"), SerializeField] private int _maxRayCount = 100;
        
        [BoxGroup("Settings"), SerializeField] private LayerMask _ignoreMask;

        private Vector3 _rayStart;
        private int _rayCount;
        
        public void Setup(float p_damage, float p_velocityOffset = 0)
        {
            _damage = p_damage;
            _speed = _baseSpeed + p_velocityOffset;
            
            _rayStart = transform.position;
            ShootRay();
        }

        private void ShootRay()
        {
            Vector3 rayEnd = _rayStart + transform.forward * _rayRange;
            if (Physics.Linecast(_rayStart, rayEnd, out RaycastHit hit, ~_ignoreMask))
            {
                hit.transform.GetComponent<IDamageable>()?.TakeDamage(_damage);
                
                Instantiate(_hitParticle, hit.point, Quaternion.LookRotation(hit.normal));
                Destroy(gameObject);
            }
            _rayStart = rayEnd;

            _rayCount++;
            if (_rayCount >= _maxRayCount)
            {
                Destroy(gameObject);
                return;
            }
            
            float delay = 1 / _speed;
            Tween tween = transform.DOMove(rayEnd, delay);
            tween.SetEase(Ease.Linear);
            tween.onComplete += ShootRay;
        }
    }
}