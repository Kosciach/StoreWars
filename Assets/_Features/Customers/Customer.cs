using UnityEngine;
using UnityEngine.AI;

namespace Kosciach.StoreWars.Customers
{
    public class Customer : MonoBehaviour
    {
        [SerializeField] private CustomerAnimatorController _animator;
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private float _rotSpeed;

        public CustomerAnimatorController Animator => _animator;
        
        private void Update()
        {
            Vector3 lookRotation = _agent.steeringTarget - transform.position;
            if(lookRotation != Vector3.zero)
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookRotation), _rotSpeed * Time.deltaTime);
        }
    }
}
