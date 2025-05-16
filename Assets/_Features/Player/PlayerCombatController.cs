using System;
using System.Collections.Generic;
using Kosciach.StoreWars.Weapons;
using UnityEngine;
using UnityEngine.InputSystem;
using NaughtyAttributes;

namespace Kosciach.StoreWars.Player
{
    using Inputs;

    public class PlayerCombatController : MonoBehaviour
    {
        private InputManager _inputManager;

        [SerializeField] private PlayerAnimatorController _animatorController;
        [SerializeField] private PlayerInventoryController _inventoryController;
        
        private bool _triggerHeld = false;

        private void Awake()
        {
            _inputManager = FindFirstObjectByType<InputManager>();

            _inputManager.InputActions.Player.UseWeapon.performed += WeaponUseStart;
            _inputManager.InputActions.Player.UseWeapon.canceled += WeaponUseEnd;
        }

        private void OnDestroy()
        {
            _inputManager.InputActions.Player.UseWeapon.performed -= WeaponUseStart;
            _inputManager.InputActions.Player.UseWeapon.canceled -= WeaponUseEnd;
        }

        private void Update()
        {
            if(_inventoryController.CurrentWeapon == null) return;
            
            _inventoryController.CurrentWeapon.UpdateWhenHeld(transform.rotation);
            if (_triggerHeld)
                _inventoryController.CurrentWeapon.HoldTrigger();
        }

        private void WeaponUseStart(InputAction.CallbackContext p_ctx)
        {
            _inventoryController.CurrentWeapon?.PressTrigger();
            _triggerHeld = true;
        }
        
        private void WeaponUseEnd(InputAction.CallbackContext p_ctx)
        {
            _triggerHeld = false;
        }
    }
}