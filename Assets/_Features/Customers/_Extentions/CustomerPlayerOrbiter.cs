using System;
using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.AI;

namespace Kosciach.StoreWars.Customers
{
    using Player;
    
    public class CustomerPlayerOrbiter : CustomerExtention
    {
        [BoxGroup("References"), SerializeField] private Player _player;
        [BoxGroup("References"), SerializeField] private BehaviorGraphAgent _behaviour;
        
        [BoxGroup("Settings"), SerializeField] private float _radius;
        [BoxGroup("Settings"), SerializeField] private int _pointsCount;
        
        [Foldout("Debug"), SerializeField, ReadOnly] private List<Vector3> _points = new();

        
        [Button]
        private void GeneratePoints()
        {
            float step = 360f / _pointsCount;
            _points.Clear();
            
            for (int i = 0; i < _pointsCount; i++)
            {
                Quaternion rotation = Quaternion.AngleAxis(step * i, Vector3.up);
                Vector3 point = rotation * (Vector3.forward * (_radius / 2f));
                
                _points.Add(point);
            }
        }

        protected override void OnTick()
        {
            Vector3 closestPoint = transform.position;
            float smallestDistance = 100;

            foreach (Vector3 point in _points)
            {
                Vector3 relativePoint = _player.transform.position + point;

                if (!NavMesh.SamplePosition(relativePoint, out NavMeshHit hit, 0.5f, NavMesh.AllAreas))
                {
                    continue;
                }
                    
                float distance = Vector3.Distance(transform.position, relativePoint);

                if (distance < smallestDistance)
                {
                    smallestDistance = distance;
                    closestPoint = relativePoint;
                }
            }
            
            _behaviour.SetVariableValue("OrbitPoint", closestPoint);
        }

#if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;

            foreach (Vector3 point in _points)
            {
                Gizmos.DrawSphere(_player.transform.position + point, 0.1f);
            }
        }
#endif
    }
}