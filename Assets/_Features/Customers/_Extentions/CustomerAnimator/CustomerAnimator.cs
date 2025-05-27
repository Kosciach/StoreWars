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

        private Tween _actionTween;
        
        protected override void OnSetup()
        {
            _animator.runtimeAnimatorController = _override;
        }

        protected override void OnTick()
        {
            bool isMoving = _agent.velocity.magnitude > 0.15f;
            _animator.SetFloat("MovementWeight", isMoving ? 1 : 0, 0.1f, Time.deltaTime);
        }

        internal float PlayAction(AnimationClip p_animation, bool p_autoStop = false, float p_playbackSpeed = 1)
        {
            _animator.ResetTrigger("ActionEnter");
            _animator.ResetTrigger("ActionExit");

            if (_actionTween != null)
            {
                _actionTween.Kill();
                _actionTween = null;
            }
            
            _animator.SetFloat("ActionSpeed", p_playbackSpeed);      
            _override["static"] = p_animation;
            _animator.SetTrigger("ActionEnter");

            float timer = p_animation.length * (1f/p_playbackSpeed);
            if (p_autoStop)
            {
                _actionTween = DOTween.To(() => timer, x => timer = x, 0, timer);
                _actionTween.OnComplete(StopAction);
            }
            
            return timer;
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