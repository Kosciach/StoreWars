using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Kosciach.StoreWars.Customers
{
    [Serializable, GeneratePropertyBag]
    [NodeDescription(name: "GoToRandomPoint", story: "[Agent] goes to a random point in [range] radius",
        category: "Action", id: "e403d156e67805ade7273212f075e28b")]
    public partial class GoToRandomPointAction : Action
    {
        [SerializeReference] public BlackboardVariable<float> Speed = new BlackboardVariable<float>(2.0f);
        [SerializeReference] public BlackboardVariable<NavMeshAgent> Agent;
        [SerializeReference] public BlackboardVariable<float> Range;

        private Vector3 _destination;

        protected override Status OnStart()
        {
            Agent.Value.speed = Speed.Value;
            Vector3 originPoint = Agent.Value.transform.position;
            Vector2 randomPoint = Random.insideUnitCircle * Range;
            _destination = originPoint + new Vector3(randomPoint.x, 0, randomPoint.y);

            NavMeshPath path = new NavMeshPath();
            if (!Agent.Value.CalculatePath(_destination, path) && path.status == NavMeshPathStatus.PathComplete)
            {
                return Status.Failure;
            }

            Agent.Value.destination = _destination;
            return Status.Running;
        }

        protected override Status OnUpdate()
        {
            float distance = Vector3.Distance(Agent.Value.transform.position, Agent.Value.destination);
            return distance <= 0.15f ? Status.Success : Status.Running;
        }

        protected override void OnEnd()
        {
        }
    }
}
