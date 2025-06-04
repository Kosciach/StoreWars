using DG.Tweening;
using Kosciach.StoreWars.Inputs;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Kosciach.StoreWars.UI
{
    public class PausePanel : UIPanel
    {
        private InputManager _inputManager;
        
        [BoxGroup("References"), SerializeField] private GameObject _playerHud;
        [BoxGroup("References"), SerializeField] private GameObject _blocker;
        [BoxGroup("References"), SerializeField] private Transform _content;
        [BoxGroup("References"), SerializeField] private SettingsPanel _settings;
        [Header("Buttons"), HorizontalLine]
        [BoxGroup("References"), SerializeField] private Button _resumeButton;
        [BoxGroup("References"), SerializeField] private Button _settingsButton;
        [BoxGroup("References"), SerializeField] private Button _exitButton;
        
        [BoxGroup("Settings"), SerializeField] private float _animTime;
        [BoxGroup("Settings"), SerializeField] private Ease _animPauseEase;
        [BoxGroup("Settings"), SerializeField] private Ease _animResumeEase;
        
        private bool _isPause;
        private Tween _animTween;
        
        
        protected override void OnLateSetup()
        {
            _inputManager = FindFirstObjectByType<InputManager>();
            _inputManager.InputActions.Player.Pause.performed += PauseInput;

            Time.timeScale = 1;
            gameObject.SetActive(false);
            _playerHud.SetActive(true);
            _blocker.SetActive(false);
            
            _resumeButton.onClick.AddListener(Resume);
            _settingsButton.onClick.AddListener(_settings.Open);
            _exitButton.onClick.AddListener(Application.Quit);
        }
        
        protected override void OnDispose()
        {
            _inputManager.InputActions.Player.Pause.performed -= PauseInput;
            
            _resumeButton.onClick.RemoveAllListeners();
            _settingsButton.onClick.RemoveAllListeners();
            _exitButton.onClick.RemoveAllListeners();
        }

        private void PauseInput(InputAction.CallbackContext p_ctx)
        {
            _isPause = !_isPause;

            if (_isPause) Pause();
            else Resume();
        }

        private void Pause()
        {
            _isPause = true;
            Time.timeScale = 0;
            
            gameObject.SetActive(true);
            _playerHud.SetActive(false);
            _blocker.SetActive(true);
            
            _content.localScale = Vector3.zero;
            _animTween = _content.DOScale(Vector3.one, 0.5f).SetUpdate(true);
            _animTween.OnComplete(() =>
            {
                _blocker.SetActive(false);
            });
            _animTween.SetEase(_animPauseEase);
        }

        private void Resume()
        {
            _isPause = false;
            _blocker.SetActive(true);
            
            _content.localScale = Vector3.one;
            _animTween = _content.DOScale(Vector3.zero, 0.5f).SetUpdate(true);
            _animTween.OnComplete(() =>
            {
                Time.timeScale = 1;
                gameObject.SetActive(false);
                _playerHud.SetActive(true);
                _blocker.SetActive(false);
            });
            _animTween.SetEase(_animResumeEase);
        }
    }

}