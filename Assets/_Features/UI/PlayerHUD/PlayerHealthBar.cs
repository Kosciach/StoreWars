using NaughtyAttributes;
using UnityEngine;
using UnityEngine.UI;

namespace Kosciach.StoreWars.UI
{
    using Player;
    
    public class PlayerHealthBar : UIPanel
    {
        private Player _player;
        private PlayerStatsController _playerStats;

        [BoxGroup("References"), SerializeField] private Slider _healthBar;
        
        
        protected override void OnLateSetup()
        {
            _player = FindFirstObjectByType<Player>();
            _playerStats = _player.GetController<PlayerStatsController>();
        }

        protected override void OnTick()
        {
            _healthBar.value = _playerStats.CurrentHealthNormalized;
        }
    }
}
