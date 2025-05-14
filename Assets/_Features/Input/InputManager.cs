using UnityEngine;

namespace Kosciach.StoreWars.Inputs
{
    public class InputManager : MonoBehaviour
    {
        private InputActions _inputActions;
        public InputActions InputActions => _inputActions;

        private void Awake()
        {
            _inputActions = new InputActions();
            _inputActions.Enable();
        }
    }
}