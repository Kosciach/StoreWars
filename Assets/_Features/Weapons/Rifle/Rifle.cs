using UnityEngine;

namespace Kosciach.StoreWars.Weapons
{
    public class Rifle : Weapon
    {
        protected override void OnPressTrigger() { }
        protected override void OnHoldTrigger() => Shoot();
    }
}