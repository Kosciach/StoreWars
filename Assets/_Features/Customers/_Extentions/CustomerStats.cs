using UnityEngine;

namespace Kosciach.StoreWars.Customers
{
    using Projectiles;
    
    public class CustomerStats : CustomerExtention, IDamageable
    {
        [SerializeField] private float _maxHealth = 100;

        private float _currentHealth;

        
        protected override void OnSetup()
        {
            _currentHealth = _maxHealth;
        }

        public void TakeDamage(float p_damage)
        {
            Debug.Log(p_damage);
            
            _currentHealth = Mathf.Max(0, _currentHealth - p_damage);
        }
    }
}