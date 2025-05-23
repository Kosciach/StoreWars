using UnityEngine;
using UnityEngine.InputSystem;
using NaughtyAttributes;

namespace Kosciach.StoreWars.Player
{
    using Inputs;

    public class PlayerEffectsController : PlayerControllerBase
    {
        private PlayerAnimatorController _animator;
        private PlayerInventoryController _inventory;

        [BoxGroup("Stun"), SerializeField] private PlayerStunViualization _stunViualization;
        
        [Foldout("Debug"), SerializeField, ReadOnly] private float _stunTimer;
        private bool _isStunned;
        public bool IsStunned => _isStunned;

        protected override void OnSetup()
        {
            _animator = _player.GetController<PlayerAnimatorController>();
            _inventory = _player.GetController<PlayerInventoryController>();
        }

        protected override void OnTick()
        {
            UpdateStun();
        }

        //Stun
        public void Stun(float p_time)
        {
            _stunTimer = p_time;
            _isStunned = true;
            _animator.SetStunLayer(true);
            _stunViualization.Show();
            _inventory.TryDropWeapon();
        }
        
        private void UpdateStun()
        {
            if (!_isStunned) return;

            _stunTimer = Mathf.Max(0, _stunTimer - Time.deltaTime);
            if (_stunTimer == 0)
            {
                _isStunned = false;
                _animator.SetStunLayer(false);
                _stunViualization.Hide();
            }
        }
    }
}