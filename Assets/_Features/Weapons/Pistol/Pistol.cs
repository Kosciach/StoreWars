using UnityEngine;

namespace Kosciach.StoreWars.Weapons
{
    public class Pistol : Weapon
    {
        protected override void OnPressTrigger() => Shoot();
        protected override void OnHoldTrigger() { }
    }
}