using UnityEngine;
using NaughtyAttributes;
using Unity.Behavior;
using Action = System.Action;

namespace Kosciach.StoreWars.Customers
{
    using Projectiles;
    
    public class CustomerStats : CustomerExtention, IDamageable
    {
        private CustomerAnimator _animator;
        
        [BoxGroup("References"), SerializeField] private BehaviorGraphAgent _behaviorGraphAgent;
        
        [BoxGroup("Stats"), SerializeField] private float _maxHealth = 100;

        private float _currentHealth;

        public event Action OnDamageTaken;
        
        
        protected override void OnSetup()
        {
            _animator = _customer.GetExtention<CustomerAnimator>();
            _currentHealth = _maxHealth;
        }

        public void TakeDamage(float p_damage)
        {
            if (_currentHealth == 0) return;
            
            _currentHealth = Mathf.Max(0, _currentHealth - p_damage);
            OnDamageTaken?.Invoke();
            
            if (_currentHealth > 0)
            {
                _animator.PlayHit();
                return;
            }

            _behaviorGraphAgent.enabled = false;
            _animator.PlayDeath();   
        }

        public void Push(Vector3 p_direction)
        {
            
        }
    }
}