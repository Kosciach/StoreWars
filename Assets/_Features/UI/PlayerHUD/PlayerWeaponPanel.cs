using TMPro;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;

namespace Kosciach.StoreWars.UI
{
    using Player;
    using Weapons;
    
    public class PlayerWeaponPanel : MonoBehaviour
    {
        private PlayerInventoryController _playerInventoryController;

        [BoxGroup("References"), SerializeField] private Image _icon;
        [BoxGroup("References"), SerializeField] private Image _iconShadow;
        [BoxGroup("References"), SerializeField] private Image _fireRateFill;
        [BoxGroup("References"), SerializeField] private TextMeshProUGUI _ammoCount;
        
        
        private void Awake()
        {
            _playerInventoryController = FindFirstObjectByType<PlayerInventoryController>();
            
            gameObject.SetActive(false);
        }
        
        private void Update()
        {
            if (_playerInventoryController.CurrentWeapon == null) return;

            Weapon weapon = _playerInventoryController.CurrentWeapon;
            _fireRateFill.fillAmount = 1 - weapon.NormalizedFireRate;
            _ammoCount.text = _playerInventoryController.CurrentWeapon.CurrentAmmo.ToString();
        }
        
        private void OnEnable()
        {
            _playerInventoryController.OnEquipWeapon += WeaponEquiped;
            _playerInventoryController.OnDropWeapon += WeaponDropped;
        }

        private void OnDisable()
        {
            _playerInventoryController.OnEquipWeapon -= WeaponEquiped;
            _playerInventoryController.OnDropWeapon -= WeaponDropped;
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
