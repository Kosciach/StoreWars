using System;
using System.Collections.Generic;
using Kosciach.StoreWars.Weapons;
using UnityEngine;
using UnityEngine.InputSystem;
using NaughtyAttributes;

namespace Kosciach.StoreWars.Player
{
    using Inputs;

    public class PlayerInventoryController : MonoBehaviour
    {
        private InputManager _inputManager;

        [SerializeField] private PlayerAnimatorController _animatorController;
        [SerializeField] private Transform _weaponHolder;
        
        private List<Weapon> _detectedWeapons = new();
        public IReadOnlyList<Weapon> DetectedWeapons => _detectedWeapons;

        private Weapon _currentWeapon;
        public Weapon CurrentWeapon => _currentWeapon;

        private void Awake()
        {
            _inputManager = FindFirstObjectByType<InputManager>();
            _inputManager.InputActions.Player.Interaction.performed += PickupWeaponInput;
            _inputManager.InputActions.Player.DropWeapon.performed += DropWeaponInput;
        }

        private void OnDestroy()
        {
            _inputManager.InputActions.Player.Interaction.performed -= PickupWeaponInput;
            _inputManager.InputActions.Player.DropWeapon.performed -= DropWeaponInput;
        }
        
        private void PickupWeaponInput(InputAction.CallbackContext p_ctx)
        {
            if (_detectedWeapons.Count == 0)
                return;
            
            TryDropWeapon();
            TryEquipWeapon();
        }
        
        private void DropWeaponInput(InputAction.CallbackContext p_ctx)
        {
            TryDropWeapon();
        }

        private void TryEquipWeapon()
        {
            if (_detectedWeapons.Count == 0) return;
            
            Weapon weapon = _detectedWeapons[0];
            _detectedWeapons.RemoveAt(0);
            
            weapon.transform.SetParent(_weaponHolder);
            weapon.transform.localPosition = Vector3.zero;
            weapon.transform.localRotation = Quaternion.identity;
            weapon.Collider.enabled = false;
            
            _animatorController.SetWeaponEquiped(true);

            _currentWeapon = weapon;
        }

        public void TryDropWeapon()
        {
            if (_currentWeapon == null) return;
            
            _currentWeapon.transform.SetParent(null);
            _currentWeapon.transform.position = transform.position;
            _currentWeapon.transform.rotation = transform.rotation * Quaternion.Euler(0, 180, 0);
            _currentWeapon.Collider.enabled = true;
            
            _animatorController.SetWeaponEquiped(false);

            _currentWeapon = null;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent<Weapon>(out Weapon weapon))
            {
                _detectedWeapons.Add(weapon);
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            if(other.TryGetComponent<Weapon>(out Weapon weapon))
            {
                _detectedWeapons.Remove(weapon);
            }
        }
    }
}