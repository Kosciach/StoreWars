using UnityEngine;

namespace Kosciach.StoreWars.Player
{
    public class PlayerAnimatorController : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        private float _movementBlendWeight;

        private void Update()
        {
            _animator.SetFloat("Movement", _movementBlendWeight, 0.1f, Time.deltaTime);
        }

        internal void MovementBlend(bool p_isMoving)
        {
            _movementBlendWeight = p_isMoving ? 1 : 0;
        }
    }
}