using UnityEngine;
using UnityEngine.AI;

namespace Kosciach.StoreWars.Customers
{
    public class Customer : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private Animator _animator;
        [SerializeField] private float _rotSpeed;
        
        
        private void Update()
        {
            bool isMoving = _agent.velocity.magnitude > 0.15f;
            _animator.SetFloat("MovementWeight", isMoving ? 1 : 0, 0.1f, Time.deltaTime);
            
            if (isMoving)
            {
                Vector3 lookRotation = _agent.steeringTarget - transform.position;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(lookRotation), _rotSpeed * Time.deltaTime);
            }
        }
    }
}
