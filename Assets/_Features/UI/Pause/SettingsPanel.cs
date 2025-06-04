using DG.Tweening;
using Kosciach.StoreWars.Inputs;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace Kosciach.StoreWars.UI
{
    public class SettingsPanel : UIPanel
    {
        [BoxGroup("References"), SerializeField] private GameObject _blocker;
        [Header("Buttons"), HorizontalLine]
        [BoxGroup("References"), SerializeField] private Button _backButton;
        
        [BoxGroup("Settings"), SerializeField] private float _animPosOffset;
        [BoxGroup("Settings"), SerializeField] private float _animTime;
        
        private Tween _animTween;
        
        
        protected override void OnLateSetup()
        {
            gameObject.SetActive(false);
            
            _backButton.onClick.AddListener(Close);
        }
        
        protected override void OnDispose()
        {
            _backButton.onClick.RemoveAllListeners();
        }
        
        internal void Open()
        {
            transform.SetSiblingIndex(0);
            transform.localPosition = Vector3.zero;
            
            gameObject.SetActive(true);
            _blocker.SetActive(true);
            
            _animTween = transform.DOLocalMoveY(_animPosOffset, _animTime/2f).SetUpdate(true);
            _animTween.SetEase(Ease.InSine);
            _animTween.OnComplete(() =>
            {
                transform.SetSiblingIndex(1);
                
                _animTween = transform.DOLocalMoveY(0, _animTime/2f).SetUpdate(true);
                _animTween.SetEase(Ease.OutSine);
                _animTween.OnComplete(() =>
                {
                    _blocker.SetActive(false);
                });
            });
        }

        internal void Close()
        {
            transform.SetSiblingIndex(1);
            transform.localPosition = Vector3.zero;
            
            gameObject.SetActive(true);
            _blocker.SetActive(true);
            
            _animTween = transform.DOLocalMoveY(_animPosOffset, _animTime/2f).SetUpdate(true);
            _animTween.SetEase(Ease.InSine);
            _animTween.OnComplete(() =>
            {
                transform.SetSiblingIndex(0);
                
                _animTween = transform.DOLocalMoveY(0, _animTime/2f).SetUpdate(true);
                _animTween.SetEase(Ease.OutSine);
                _animTween.OnComplete(() =>
                {
                    gameObject.SetActive(false);
                    _blocker.SetActive(false);
                });
            });
        }
    }

}