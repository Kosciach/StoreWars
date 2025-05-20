using System;
using UnityEngine;

namespace Kosciach.StoreWars.Player
{
    public class Player : MonoBehaviour
    {
        public PlayerMovementController Movement { get; private set; }
        public PlayerAnimatorController Animator { get; private set; }
        public PlayerInventoryController Inventory { get; private set; }
        public PlayerCombatController Combat { get; private set; }

        private void Awake()
        {
            Movement = GetComponent<PlayerMovementController>();
            Animator = GetComponent<PlayerAnimatorController>();
            Inventory = GetComponent<PlayerInventoryController>();
            Combat = GetComponent<PlayerCombatController>();

            Movement.Setup(this);
            Animator.Setup(this);
            Inventory.Setup(this);
            Combat.Setup(this);
        }

        private void OnDestroy()
        {
            Movement.Dispose();
            Animator.Dispose();
            Inventory.Dispose();
            Combat.Dispose();
        }

        private void Update()
        {
            Movement.Tick();
            Animator.Tick();
            Inventory.Tick();
            Combat.Tick();
        }
    }
}