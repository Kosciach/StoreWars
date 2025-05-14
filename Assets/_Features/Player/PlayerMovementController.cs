using UnityEngine;
using UnityEngine.InputSystem;
using NaughtyAttributes;

namespace Kosciach.StoreWars.Player
{
    using Inputs;

    public class PlayerMovementController : MonoBehaviour
    {
        private InputManager _inputManager;

        [BoxGroup("References"), SerializeField] private PlayerAnimatorController _animatorController;
        [BoxGroup("References"), SerializeField] private CharacterController _characterController;

        [BoxGroup("Movement"), SerializeField] private float _speed;
        [BoxGroup("Movement"), SerializeField] private float _movementDamping;

        [BoxGroup("LookAt"), SerializeField] private float _lookAtLerpSpeed;
        
        private Vector3 _movementInput;
        private Vector3 _currentVelocity;
        private Vector3 _refVelocity;

        private void Awake()
        {
            _inputManager = FindFirstObjectByType<InputManager>();

            _inputManager.InputActions.Player.Movement.performed += MovementInput;
            _inputManager.InputActions.Player.Movement.canceled += MovementInput;
        }

        private void OnDestroy()
        {
            _inputManager.InputActions.Player.Movement.performed -= MovementInput;
            _inputManager.InputActions.Player.Movement.canceled -= MovementInput;
        }

        private void Update()
        {
            //Move
            Vector3 targetVelocity = _movementInput * _speed;
            _currentVelocity = Vector3.SmoothDamp(_currentVelocity, targetVelocity, ref _refVelocity, _movementDamping * 100 * Time.deltaTime);
            _characterController.Move(_currentVelocity * Time.deltaTime);

            //Rotate
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask("MouseTarget")))
            {
                Vector3 targetPosition = hit.point;
                targetPosition.y = transform.position.y;

                Vector3 direction = (targetPosition - transform.position).normalized;

                Quaternion targetLookRotation = Quaternion.LookRotation(direction);
                
                transform.rotation = Quaternion.Lerp(transform.rotation, targetLookRotation, Time.deltaTime * _lookAtLerpSpeed);
            }
        }

        private void MovementInput(InputAction.CallbackContext p_ctx)
        {
            Vector2 input = p_ctx.ReadValue<Vector2>();

            _movementInput = new Vector3(input.x, 0, input.y);
            _animatorController.MovementBlend(_movementInput.magnitude > 0f);
        }
    }
}