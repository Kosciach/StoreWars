using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Kosciach.StoreWars.UI
{
    public class ButtonHoverScaler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Vector3 _hoverScale;
        [SerializeField] private float _scaleTime;

        private Vector3 _defaultScale;
        private Tween _tween;

        private void Awake()
        {
            _defaultScale = transform.localScale;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _tween?.Kill();
            _tween = transform.DOScale(_hoverScale, _scaleTime).SetUpdate(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _tween?.Kill();
            _tween = transform.DOScale(_defaultScale, _scaleTime).SetUpdate(true);
        }

        private void OnDisable()
        {
            transform.localScale = _defaultScale;
        }
    }

}