using System;
using Kosciach.StoreWars.Customers;
using Kosciach.StoreWars.Player;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "BratTryAttack", story: "Brat counts down and at 0 attack", category: "Action", id: "e8119fd24785d4c48ca57b6e3f567b0b")]
public partial class BratTryAttackAction : Action
{
    [SerializeReference] public BlackboardVariable<Customer> Brat;
    [SerializeReference] public BlackboardVariable<Player> Player;
    [SerializeReference] public BlackboardVariable<AnimationClip> Anim;
    [SerializeReference] public BlackboardVariable<BratProjectile> ProjectilePrefab;
    [SerializeReference] public BlackboardVariable<float> TimeBetweenAttacks;
    
    private float _timer = -1;
    
    
    protected override Status OnStart()
    {
        //First Start
        if (_timer < 0)
        {
            _timer = TimeBetweenAttacks.Value;
        }
        
        _timer = Mathf.Max(0, _timer - Time.deltaTime);
        if (_timer == 0)
        {
            _timer = TimeBetweenAttacks.Value;
            Brat.Value.GetExtention<CustomerAnimator>().PlayAction(Anim, true);

            Transform bratTransform = Brat.Value.transform;
            Vector3 spawnPos = bratTransform.position + Vector3.up / 4f;
            Quaternion spawnRot = bratTransform.rotation;
            BratProjectile projectile = GameObject.Instantiate(ProjectilePrefab.Value, spawnPos, spawnRot);
            projectile.Setup(Player.Value);
        }
        
        return Status.Success;
    }

    protected override Status OnUpdate()
    {
        return Status.Success;
    }

    protected override void OnEnd()
    {
    }
}

