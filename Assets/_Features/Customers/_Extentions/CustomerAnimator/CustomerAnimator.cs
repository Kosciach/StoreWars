using DG.Tweening;
using NaughtyAttributes;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.AI;

namespace Kosciach.StoreWars.Customers
{
    public class CustomerAnimator : CustomerExtention
    {
        [BoxGroup("References"), SerializeField] private NavMeshAgent _agent;
        [BoxGroup("References"), SerializeField] private Animator _animator;
        [BoxGroup("References"), SerializeField] private AnimatorOverrideController _override;
        
        
        protected override void OnSetup()
        {
            _animator.runtimeAnimatorController = _override;
        }

        protected override void OnTick()
        {
            bool isMoving = _agent.velocity.magnitude > 0.15f;
            _animator.SetFloat("MovementWeight", isMoving ? 1 : 0, 0.1f, Time.deltaTime);
        }

        internal void PlayAction(AnimationClip p_animation, bool p_loop = false)
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
        
        internal void PlayHit()
        {
            _animator.SetTrigger("Hit");
        }
        
        internal void PlayDeath()
        {
            _animator.SetTrigger("Die");
        }
        
        public void AnimEvent(string p_eventName)
        {
            if (p_eventName == "DeathEnd")
            {
                Destroy(gameObject);
            }
        }
    }
}