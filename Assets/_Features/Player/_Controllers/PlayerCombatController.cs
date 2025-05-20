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
        
        private bool _triggerHeld = false;

        internal override void OnSetup()
        {
            _inputManager = FindFirstObjectByType<InputManager>();

            _inputManager.InputActions.Player.UseWeapon.performed += WeaponUseStart;
            _inputManager.InputActions.Player.UseWeapon.canceled += WeaponUseEnd;
        }

        internal override void OnDispose()
        {
            _inputManager.InputActions.Player.UseWeapon.performed -= WeaponUseStart;
            _inputManager.InputActions.Player.UseWeapon.canceled -= WeaponUseEnd;
        }

        internal override void OnTick()
        {
            if(_player.Inventory.CurrentWeapon == null) return;
            
            _player.Inventory.CurrentWeapon.UpdateWhenHeld(transform.rotation);
            if (_triggerHeld)
                _player.Inventory.CurrentWeapon.HoldTrigger();
        }

        private void WeaponUseStart(InputAction.CallbackContext p_ctx)
        {
            _player.Inventory.CurrentWeapon?.PressTrigger();
            _triggerHeld = true;
        }
        
        private void WeaponUseEnd(InputAction.CallbackContext p_ctx)
        {
            _triggerHeld = false;
        }
    }
}