using TMPro;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;

namespace Kosciach.StoreWars.UI
{
    using Player;
    using Weapons;
    
    public class PlayerWeaponPanel : UIPanel
    {
        private PlayerInventoryController _playerInventoryController;

        [BoxGroup("References"), SerializeField] private Image _icon;
        [BoxGroup("References"), SerializeField] private Image _iconShadow;
        [BoxGroup("References"), SerializeField] private Image _fireRateFill;
        [BoxGroup("References"), SerializeField] private TextMeshProUGUI _ammoCount;
        
        
        protected override void OnSetup()
        {
            _playerInventoryController = FindFirstObjectByType<PlayerInventoryController>();
            _playerInventoryController.OnEquipWeapon += WeaponEquiped;
            _playerInventoryController.OnDropWeapon += WeaponDropped;
            
            gameObject.SetActive(false);
        }
        
        protected override void OnDispose()
        {
            _playerInventoryController.OnEquipWeapon -= WeaponEquiped;
            _playerInventoryController.OnDropWeapon -= WeaponDropped;
        }
        
        protected override void OnTick()
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
