using DG.Tweening;
using NaughtyAttributes;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.AI;

namespace Kosciach.StoreWars.Customers
{
    public class CustomerAnimator_EventsReceiver : MonoBehaviour
    {
        [SerializeField] private CustomerAnimator _animator;
        
        public void AnimEvent(string p_eventName) => _animator.AnimEvent(p_eventName);
    }
}