using UnityEngine;
using UnityEngine.InputSystem;
using NaughtyAttributes;

namespace Kosciach.StoreWars.Player
{
    using Inputs;

    public class PlayerStatsController : PlayerControllerBase
    {

        [BoxGroup("Stats"), SerializeField] private float _maxHealth;
        
        private float _currentHealth;
        public float CurrentHealthNormalized => _currentHealth / _maxHealth;

        protected override void OnSetup()
        {
            _currentHealth = _maxHealth;
        }

        public void TakeDamage(float p_damage)
        {
            _currentHealth = Mathf.Max(0, _currentHealth - p_damage);
        }
    }
}