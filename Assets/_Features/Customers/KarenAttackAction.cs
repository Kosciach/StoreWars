using System;
using Kosciach.StoreWars.Customers;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "KarenAttack", story: "Karen attack Target", category: "Action", id: "f7900f223a4f4f08058c658151aea6ff")]
public partial class KarenAttackAction : Action
{
    [SerializeReference] public BlackboardVariable<AnimationClip> AttackAnim;
    [SerializeReference] public BlackboardVariable<Customer> Karen;
    [SerializeReference] public BlackboardVariable<bool> IsPlayerDetected;

    private float _timer = 5;
    
    protected override Status OnStart()
    {
        Karen.Value.Animator.PlayAnim(AttackAnim.Value);
        _timer = 5;
        
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        _timer = Mathf.Max(0, _timer - Time.deltaTime);
        return _timer == 0 ? Status.Success : Status.Running;
    }

    protected override void OnEnd()
    {
        Karen.Value.Animator.StopAction();
    }
}

