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

        }

        private void WeaponUseStart(InputAction.CallbackContext p_ctx)
        {
            _triggerHeld = true;
        }
        
        private void WeaponUseEnd(InputAction.CallbackContext p_ctx)
        {
            _triggerHeld = false;
        }
    }
}