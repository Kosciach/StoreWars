using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "KarenAttack", story: "Karen attack Target", category: "Action", id: "f7900f223a4f4f08058c658151aea6ff")]
public partial class KarenAttackAction : Action
{
    [SerializeReference] public BlackboardVariable<bool> IsPlayerDetected;
    
    protected override Status OnStart()
    {
        Debug.Log("Attack!");
        
        IsPlayerDetected.Value = false;
        
        return Status.Success;
    }

    protected override Status OnUpdate()
    {
        return Status.Running;
    }

    protected override void OnEnd()
    {
    }
}

