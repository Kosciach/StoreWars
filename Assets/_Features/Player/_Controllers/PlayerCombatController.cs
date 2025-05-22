using System;
using System.Collections.Generic;
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
        
        private bool _triggerHeld = false;

        protected override void OnSetup()
        {
            _inputManager = FindFirstObjectByType<InputManager>();
            _inputManager.InputActions.Player.UseWeapon.performed += WeaponUseStart;
            _inputManager.InputActions.Player.UseWeapon.canceled += WeaponUseEnd;

            _inventory = _player.GetController<PlayerInventoryController>();
        }

        protected override void OnDispose()
        {
            _inputManager.InputActions.Player.UseWeapon.performed -= WeaponUseStart;
            _inputManager.InputActions.Player.UseWeapon.canceled -= WeaponUseEnd;
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
    }
}