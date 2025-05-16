using System;
using Kosciach.StoreWars.Player;
using Kosciach.StoreWars.Weapons;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Kosciach.StoreWars.UI
{
    public class PlayerWeaponPanel : MonoBehaviour
    {
        private PlayerInventoryController _playerInventoryController;

        [SerializeField] private Image _icon;
        [SerializeField] private Image _iconShadow;
        [SerializeField] private Image _fireRateFill;
        [SerializeField] private TextMeshProUGUI _ammoCount;
        
        internal void Setup()
        {
            _playerInventoryController = FindFirstObjectByType<PlayerInventoryController>();
            _playerInventoryController.OnEquipWeapon += WeaponEquiped;
            _playerInventoryController.OnDropWeapon += WeaponDropped;
            
            gameObject.SetActive(false);
        }
        
        internal void Dispose()
        {
            _playerInventoryController.OnEquipWeapon -= WeaponEquiped;
            _playerInventoryController.OnDropWeapon -= WeaponDropped;
        }

        private void Update()
        {
            if (_playerInventoryController.CurrentWeapon == null) return;

            Weapon weapon = _playerInventoryController.CurrentWeapon;
            _fireRateFill.fillAmount = 1 - weapon.NormalizedFireRate;
            _ammoCount.text = _playerInventoryController.CurrentWeapon.CurrentAmmo.ToString();
        }

        private void WeaponEquiped()
        {
            _icon.sprite = _playerInventoryController.CurrentWeapon.Icon;
            _iconShadow.sprite = _icon.sprite;
            
            gameObject.SetActive(true);
        }
        
        private void WeaponDropped()
        {
            gameObject.SetActive(false);
        }
    }
}
