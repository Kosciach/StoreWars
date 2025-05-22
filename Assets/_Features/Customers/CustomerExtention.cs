using UnityEngine;

namespace Kosciach.StoreWars.Customers
{
    public abstract class CustomerExtention : MonoBehaviour
    {
        protected Customer _customer { get; private set; }

        internal void Setup(Customer p_customer)
        {
            _customer = p_customer;

            OnSetup();
        }

        internal void Dispose() => OnDispose();
        
        internal void Tick() => OnTick();
        
        
        protected virtual void OnSetup() { }
        protected virtual void OnDispose() { }
        protected virtual void OnTick() { }
    }
}
