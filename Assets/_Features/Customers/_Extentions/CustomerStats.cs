using System;
using Kosciach.StoreWars.Damageable;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.AI;

namespace Kosciach.StoreWars.Customers
{
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