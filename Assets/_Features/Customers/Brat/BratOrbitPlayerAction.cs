using System;
using Kosciach.StoreWars.Customers;
using Kosciach.StoreWars.Player;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "BratOrbitPlayer", story: "Brat orbits Player", category: "Action", id: "e49805de3e1833a3027cb8a310062fe1")]
public partial class BratOrbitPlayerAction : Action
{
    [SerializeReference] public BlackboardVariable<Customer> Brat;
    [SerializeReference] public BlackboardVariable<Player> Player;
    [SerializeReference] public BlackboardVariable<Vector3> OrbitPoint;
    
    protected override Status OnStart()
    {
        Brat.Value.Agent.destination = OrbitPoint.Value;
        Brat.Value.transform.LookAt(Player.Value.transform);
        
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

