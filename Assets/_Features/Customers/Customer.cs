using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Kosciach.StoreWars.Customers
{
    public class Customer : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;
        public NavMeshAgent Agent => _agent;
        
        private Dictionary<Type, CustomerExtention> _extentions = new();

        
        private void Awake()
        {
            foreach (CustomerExtention extention in GetComponents<CustomerExtention>())
            {
                _extentions.Add(extention.GetType(), extention);
            }
            
            foreach ((Type type, CustomerExtention extention) in _extentions)
            {
                extention.Setup(this);
            }
        }

        private void OnDestroy()
        {
            foreach ((Type type, CustomerExtention extention) in _extentions)
            {
                extention.Dispose();
            }
        }

        private void Update()
        {
            foreach ((Type type, CustomerExtention extention) in _extentions)
            {
                extention.Tick();
            }
        }

        public T GetExtention<T>() where T : CustomerExtention
        {
            return _extentions[typeof(T)] as T;
        }
    }
}
