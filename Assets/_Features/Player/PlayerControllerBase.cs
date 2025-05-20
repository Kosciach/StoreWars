using System;
using UnityEngine;

namespace Kosciach.StoreWars.Player
{
    public class PlayerControllerBase : MonoBehaviour
    {
        protected Player _player { get; private set; }

        internal void Setup(Player p_player)
        {
            _player = p_player;

            OnSetup();
        }

        internal void Dispose() => OnDispose();
        
        internal void Tick() => OnTick();
        
        
        internal virtual void OnSetup() { }
        internal virtual void OnDispose() { }
        internal virtual void OnTick() { }
    }
}