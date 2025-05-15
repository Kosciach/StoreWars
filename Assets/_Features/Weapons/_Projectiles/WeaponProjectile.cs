using System;
using NaughtyAttributes;
using UnityEngine;

namespace Kosciach.StoreWars.Weapons.Projectiles
{
    [RequireComponent(typeof(Rigidbody))]
    public abstract class WeaponProjectile : MonoBehaviour
    {
        private float _damage;
        
        [BoxGroup("References"), SerializeField] private Rigidbody _rigidbody;
        [BoxGroup("References"), SerializeField] private ParticleSystem _hitParticle;

        [BoxGroup("Stats"), SerializeField] private float _initialVelocity;
        
        
        internal void Setup(float p_damage)
        {
            _damage = p_damage;
            
            _rigidbody.AddForce(transform.forward * _initialVelocity, ForceMode.Impulse);
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.transform.CompareTag("Player") || other.transform.CompareTag("Weapon")) return;
            
            Instantiate(_hitParticle, transform.position, Quaternion.LookRotation(other.contacts[0].normal));
            
            Destroy(gameObject);
        }
    }
}