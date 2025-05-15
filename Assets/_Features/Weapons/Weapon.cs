using UnityEngine;

namespace Kosciach.StoreWars.Weapons
{
    [RequireComponent(typeof(BoxCollider))]
    public abstract class Weapon : MonoBehaviour
    {
        private BoxCollider _collider;
        public BoxCollider Collider => _collider;
        
        private void Awake()
        {
            _collider = GetComponent<BoxCollider>();
        }
    }
}