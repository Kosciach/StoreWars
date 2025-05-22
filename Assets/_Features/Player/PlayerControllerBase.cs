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
        
        
        protected virtual void OnSetup() { }
        protected virtual void OnDispose() { }
        protected virtual void OnTick() { }
    }
}