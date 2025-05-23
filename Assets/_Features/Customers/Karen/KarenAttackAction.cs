using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

namespace Kosciach.StoreWars.Customers
{
    using Player;
    
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "KarenAttack", story: "Karen attacks Player", category: "Action", id: "f7900f223a4f4f08058c658151aea6ff")]
    public partial class KarenAttackAction : Action
    {
        [SerializeReference] public BlackboardVariable<Customer> Karen;
        [SerializeReference] public BlackboardVariable<Player> Player;
        [SerializeReference] public BlackboardVariable<AnimationClip> AttackAnim;
        [SerializeReference] public BlackboardVariable<float> Duration;
        [SerializeReference] public BlackboardVariable<float> StunTime;

        private CustomerAnimator _animator;
        private float _timer;
        
        protected override Status OnStart()
        {
            //Assign
            _animator = Karen.Value.GetExtention<CustomerAnimator>();
            _timer = Duration.Value;
            
            //Customer - Karen
            Karen.Value.Agent.destination = Karen.Value.transform.position;
            _animator.PlayAction(AttackAnim.Value);
            
            //Player
            Player.Value.GetController<PlayerEffectsController>().Stun(StunTime.Value);
            
            return Status.Running;
        }

        protected override Status OnUpdate()
        {
            _timer = Mathf.Max(0, _timer - Time.deltaTime);
            return _timer == 0 ? Status.Success : Status.Running;
        }

        protected override void OnEnd()
        {
            _animator .StopAction();
        }
    }
}
