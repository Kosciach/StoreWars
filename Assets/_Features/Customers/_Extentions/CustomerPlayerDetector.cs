using System.Collections;
using NaughtyAttributes;
using Unity.Behavior;
using UnityEngine;

namespace Kosciach.StoreWars.Customers
{
    using Player;
    
    public class CustomerPlayerDetector : CustomerExtention
    {
        [BoxGroup("References"), SerializeField] private BehaviorGraphAgent _behaviourAgent;

        [BoxGroup("Settings"), SerializeField] private LayerMask _playerMask;
        [BoxGroup("Settings"), SerializeField, Range(0, 5)] private float _range;
        [BoxGroup("Settings"), SerializeField, Range(0, 360)] private float _angle;
        
        private Transform _playerTransform;
        
        internal float Range => _range;
        internal float Angle => _angle;
        internal Transform PlayerTransform => _playerTransform;

        private void Awake()
        {
            StartCoroutine(DetectionCoroutine());
        }

        private IEnumerator DetectionCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(0.2f);
                TryDetectPlayer();
            }
        }

        private void TryDetectPlayer()
        {
            //Main Detect
            Collider[] colliders = Physics.OverlapSphere(transform.position, _range, _playerMask);
            if (colliders.Length == 0)
            {
                _playerTransform = null;
                return;
            }

            //Check FOV
            Transform playerTransform = colliders[0].transform;
            Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, directionToPlayer) >= _angle / 2f)
            {
                _playerTransform = null;
                return;
            }

            //Check Raycast
            if (Physics.Linecast(transform.position, playerTransform.position, ~_playerMask))
            {
                _playerTransform = null;
                return;
            }
            
            //Check is Stunned
            Player player = playerTransform.GetComponent<Player>();
            if (player.GetController<PlayerEffectsController>().IsStunned)
            {
                _playerTransform = null;
                return;
            }
            
            _playerTransform = playerTransform;
            _behaviourAgent.SetVariableValue("IsPlayerDetected", true);
        }
    }
}