using System;
using NaughtyAttributes;
using UnityEngine;

namespace Kosciach.StoreWars.Customers
{
    using Player;
    
    public class BratProjectile : MonoBehaviour
    {
        private Player _player;

        [BoxGroup("References"), SerializeField] private ParticleSystem _hitParticle;
        [BoxGroup("References"), SerializeField] private Rigidbody _rigidbody;
        
        [BoxGroup("Settings"), SerializeField] private float _speed;
        [BoxGroup("Settings"), SerializeField] private float _damage;
            
        internal void Setup(Player p_player)
        {
            _player = p_player;
            
            _rigidbody.AddForce(transform.forward * _speed, ForceMode.Impulse);
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.transform.CompareTag("Player")) return;

            _player.GetController<PlayerStatsController>().TakeDamage(_damage);

            Instantiate(_hitParticle, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}