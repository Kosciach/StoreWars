using System;
using UnityEngine;
using UnityEngine.AI;

namespace Kosciach.StoreWars.Customers
{
    public class CustomerAnimatorController : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private Animator _animator;

        [SerializeField] private AnimatorOverrideController _override;

        private void Awake()
        {
            _animator.runtimeAnimatorController = _override;
        }

        private void Update()
        {
            bool isMoving = _agent.velocity.magnitude > 0.15f;
            _animator.SetFloat("MovementWeight", isMoving ? 1 : 0, 0.1f, Time.deltaTime);
        }

        internal void PlayAnim(AnimationClip p_animation, bool p_loop = false)
        {
            _override["static"] = p_animation;
            _animator.SetTrigger("ActionEnter");
            
            if (p_loop)
                StopAction();
        }

        internal void StopAction()
        {
            _animator.SetTrigger("ActionExit");
        }
    }
}