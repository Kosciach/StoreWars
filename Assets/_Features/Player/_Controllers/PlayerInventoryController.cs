using System;
using System.Collections.Generic;
using Kosciach.StoreWars.Weapons;
using UnityEngine;
using UnityEngine.InputSystem;
using NaughtyAttributes;

namespace Kosciach.StoreWars.Player
{
    using Inputs;

    public class PlayerInventoryController : PlayerControllerBase
    {
        private InputManager _inputManager;
        private PlayerAnimatorController _animator;
        
        [SerializeField] private Transform _weaponHolder;
        
        //Weapons
        private List<Weapon> _detectedWeapons = new();
        public IReadOnlyList<Weapon> DetectedWeapons => _detectedWeapons;

        private Weapon _currentWeapon;
        public Weapon CurrentWeapon => _currentWeapon;

        //Events
        public event Action OnEquipWeapon;
        public event Action OnDropWeapon;

        internal override void OnSetup()
        {
            _inputManager = FindFirstObjectByType<InputManager>();
            _inputManager.InputActions.Player.Interaction.performed += PickupWeaponInput;
            _inputManager.InputActions.Player.DropWeapon.performed += DropWeaponInput;
            
            _animator = _player.GetController<PlayerAnimatorController>();
        }

        internal override void OnDispose()
        {
            _inputManager.InputActions.Player.Interaction.performed -= PickupWeaponInput;
            _inputManager.InputActions.Player.DropWeapon.performed -= DropWeaponInput;
        }
        
        private void PickupWeaponInput(InputAction.CallbackContext p_ctx)
        {
            if (_detectedWeapons.Count > 0)
            {
                TryDropWeapon();
                TryEquipWeapon(); 
            }
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
            weapon.transform.localPosition = weapon.InHandOffset;
            weapon.transform.localRotation = Quaternion.identity;
            weapon.Collider.enabled = false;
            
            _animator.SetWeaponEquiped(true);

            _currentWeapon = weapon;
            
            _currentWeapon.Equip(_animator.Shoot);
            _animator.SetRecoil(_currentWeapon.Recoil, _currentWeapon.RecoilTime);
            
            OnEquipWeapon?.Invoke();
        }

        public void TryDropWeapon()
        {
            if (_currentWeapon == null) return;
            
            _currentWeapon.transform.SetParent(null);
            _currentWeapon.transform.position = transform.position + new Vector3(0, 0.1f, 0);
            _currentWeapon.transform.rotation = transform.rotation * Quaternion.Euler(0, 180, 0);
            _currentWeapon.Collider.enabled = true;
            
            _animator.SetWeaponEquiped(false);

            _currentWeapon.UnEquip();
            
            _currentWeapon = null;
            
            OnDropWeapon?.Invoke();
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent<Weapon>(out Weapon weapon))
            {
                _detectedWeapons.Add(weapon);
            }

            if (other.CompareTag("AmmoPile") && _currentWeapon != null && _currentWeapon.CurrentAmmo != _currentWeapon.MaxAmmo)
            {
                _currentWeapon.Reload();
                Destroy(other.gameObject);
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