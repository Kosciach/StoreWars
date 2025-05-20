using System;
using System.Collections.Generic;
using UnityEngine;

namespace Kosciach.StoreWars.Player
{
    public class Player : MonoBehaviour
    {
        private Dictionary<Type, PlayerControllerBase> _controllers = new();

        private void Awake()
        {
            foreach (PlayerControllerBase controller in GetComponents<PlayerControllerBase>())
            {
                _controllers.Add(controller.GetType(), controller);
            }
            
            foreach ((Type type, PlayerControllerBase controller) in _controllers)
            {
                controller.Setup(this);
            }
        }

        private void OnDestroy()
        {
            foreach ((Type type, PlayerControllerBase controller) in _controllers)
            {
                controller.Dispose();
            }
        }

        private void Update()
        {
            foreach ((Type type, PlayerControllerBase controller) in _controllers)
            {
                controller.Tick();
            }
        }

        public T GetController<T>() where T : PlayerControllerBase
        {
            return _controllers[typeof(T)] as T;
        }
    }
}