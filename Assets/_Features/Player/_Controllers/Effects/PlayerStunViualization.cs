using System;
using UnityEngine;
using UnityEngine.InputSystem;
using NaughtyAttributes;

namespace Kosciach.StoreWars.Player
{
    using Inputs;

    public class PlayerStunViualization : MonoBehaviour
    {
        [SerializeField] private Transform _orb1;
        [SerializeField] private Transform _orb2;
        [SerializeField] private float _orbitRadius = 0.5f;
        [SerializeField] private float _orbitSpeed = 3f;
        
        private void Awake()
        {
            Hide();
        }

        private void Update()
        {
            _orb1.localPosition = new Vector3(_orbitRadius/2f, 0, 0);
            _orb2.localPosition = -new Vector3(_orbitRadius/2f, 0, 0);

            Transform camera = Camera.main.transform;
            _orb1.LookAt(camera.position);
            _orb2.LookAt(camera.position);
            
            transform.Rotate(Vector3.up, _orbitSpeed * 100 * Time.deltaTime);
        }

        internal void Show()
        {
            gameObject.SetActive(true);
        }

        internal void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}