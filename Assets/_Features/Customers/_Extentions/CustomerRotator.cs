using System.Collections;
using NaughtyAttributes;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.AI;

namespace Kosciach.StoreWars.Customers
{
    using Player;
    
    public class CustomerRotator : CustomerExtention
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private float _rotSpeed;
        
        private void Update()
        {
            Vector3 lookRotation = _agent.steeringTarget - transform.position;
            if(lookRotation != Vector3.zero)
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookRotation), _rotSpeed * Time.deltaTime);
        }
    }
}