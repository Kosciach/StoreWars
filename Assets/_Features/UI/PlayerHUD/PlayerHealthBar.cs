using System;
using Kosciach.StoreWars.Player;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

namespace Kosciach.StoreWars.UI
{
    using Player;
    
    public class PlayerHealthBar : MonoBehaviour
    {
        private Player _player;
        private PlayerStatsController _playerStats;

        [BoxGroup("References"), SerializeField] private Slider _healthBar;
        
        
        private void Start()
        {
            _player = FindFirstObjectByType<Player>();
            _playerStats = _player.GetController<PlayerStatsController>();
        }
        
        internal void Dispose()
        {

        }

        private void Update()
        {
            _healthBar.value = _playerStats.CurrentHealthNormalized;
        }
    }
}
