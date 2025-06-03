using Kosciach.StoreWars.Inputs;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Kosciach.StoreWars.UI
{
    public class PausePanel : UIPanel
    {
        private InputManager _inputManager;

        private bool _isPause;
        
        
        protected override void OnLateSetup()
        {
            _inputManager = FindFirstObjectByType<InputManager>();
            _inputManager.InputActions.Player.Pause.performed += PauseInput;

            Resume();
        }
        
        protected override void OnDispose()
        {
            _inputManager.InputActions.Player.Pause.performed -= PauseInput;
        }

        private void PauseInput(InputAction.CallbackContext p_ctx)
        {
            _isPause = !_isPause;

            if (_isPause) Pause();
            else Resume();
        }

        private void Pause()
        {
            Time.timeScale = 0;
            gameObject.SetActive(true);
        }

        private void Resume()
        {
            Time.timeScale = 1;
            gameObject.SetActive(false);
        }
    }

}