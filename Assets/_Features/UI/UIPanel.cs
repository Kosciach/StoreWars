using UnityEngine;

namespace Kosciach.StoreWars.UI
{
    public abstract class UIPanel : MonoBehaviour
    {
        internal void Setup() => OnSetup();
        internal void LateSetup() => OnLateSetup();
        internal void Tick() => OnTick();
        internal void Dispose() => OnDispose();

        protected virtual void OnSetup() { }
        protected virtual void OnLateSetup() { }
        protected virtual void OnTick() { }
        protected virtual void OnDispose() { }
    }
}
