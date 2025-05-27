using System;
using Kosciach.StoreWars.Player;
using UnityEngine;

namespace Kosciach.StoreWars.UI
{
    public class PlayerHUD : MonoBehaviour
    {
        [SerializeField] private PlayerWeaponPanel _weaponPanel;
        [SerializeField] private PlayerHealthBar _healthBar;
        
        private void Awake()
        {
            _weaponPanel.Setup();
        }
        
        private void OnDestroy()
        {
            _weaponPanel.Dispose();
            _healthBar.Dispose();
        }
    }
}
