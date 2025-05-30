using System;
using System.Collections.Generic;
using Kosciach.StoreWars.Projectiles;
using Kosciach.StoreWars.Weapons;
using UnityEngine;
using UnityEngine.InputSystem;
using NaughtyAttributes;

namespace Kosciach.StoreWars.Player
{
    using Inputs;

    public class PlayerCombatController : PlayerControllerBase
    {
        private InputManager _inputManager;
        private PlayerInventoryController _inventory;
        private PlayerAnimatorController _animator;

        [BoxGroup("Settings"), SerializeField] private float _meleeRange;
        [BoxGroup("Settings"), SerializeField] private float _meleeKnockback;
        
        private bool _triggerHeld = false;

        protected override void OnSetup()
        {
            _inputManager = FindFirstObjectByType<InputManager>();
            _inputManager.InputActions.Player.UseWeapon.performed += WeaponUseStart;
            _inputManager.InputActions.Player.UseWeapon.canceled += WeaponUseEnd;
            _inputManager.InputActions.Player.Melee.canceled += Melee;

            _inventory = _player.GetController<PlayerInventoryController>();
            _animator = _player.GetController<PlayerAnimatorController>();
        }

        protected override void OnDispose()
        {
            _inputManager.InputActions.Player.UseWeapon.performed -= WeaponUseStart;
            _inputManager.InputActions.Player.UseWeapon.canceled -= WeaponUseEnd;
            _inputManager.InputActions.Player.Melee.canceled -= Melee;
        }

        protected override void OnTick()
        {
            if(_inventory.CurrentWeapon == null) return;
            
            _inventory.CurrentWeapon.UpdateWhenHeld(transform.rotation);
            if (_triggerHeld)
                _inventory.CurrentWeapon.HoldTrigger();
        }

        private void WeaponUseStart(InputAction.CallbackContext p_ctx)
        {
            _inventory.CurrentWeapon?.PressTrigger();
            _triggerHeld = true;
        }
        
        private void WeaponUseEnd(InputAction.CallbackContext p_ctx)
        {
            _triggerHeld = false;
        }
        
        private void Melee(InputAction.CallbackContext p_ctx)
        {
            _animator.Melee();
            
            Debug.DrawRay(transform.position + Vector3.up/4f, transform.forward * _meleeRange, Color.red, 5);
            if (Physics.Raycast(transform.position + Vector3.up / 4f, transform.forward, out RaycastHit hit, _meleeRange, LayerMask.GetMask("Customer")))
            {
                hit.transform.GetComponent<IDamageable>().Push(transform.forward * _meleeKnockback);
            }
        }
    }
}