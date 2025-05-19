using System;
using System.IO;
using PlasticPipe.Server;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEngine.AI;
using Random = UnityEngine.Random;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "GoToRandomPoint", story: "[Someone] goes to a random point in [range] radius", category: "Action", id: "e403d156e67805ade7273212f075e28b")]
public partial class GoToRandomPointAction : Action
{
    [SerializeReference] public BlackboardVariable<UnityEngine.AI.NavMeshAgent> Someone;
    [SerializeReference] public BlackboardVariable<float> Range;

    private Vector3 _destination;
    
    protected override Status OnStart()
    {
        NavMeshAgent agent = Someone.Value;
        Vector3 originPoint = agent.transform.position;
        Vector2 randomPoint = Random.insideUnitCircle * Range;
        _destination = originPoint + new Vector3(randomPoint.x, 0, randomPoint.y);

        NavMeshPath path = new NavMeshPath();
        if (!agent.CalculatePath(_destination, path) && path.status == NavMeshPathStatus.PathComplete)
        {
            return Status.Failure;
        }
        
        agent.destination = _destination;
        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        float distance = Vector3.Distance(Someone.Value.transform.position, Someone.Value.destination);
        return distance <= 0.15f ? Status.Success : Status.Running;
    }

    protected override void OnEnd()
    {
    }
}

