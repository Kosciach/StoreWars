using DG.Tweening;
using UnityEngine;

namespace Kosciach.StoreWars.Player
{
    public class PlayerAnimatorController : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        [SerializeField] private float _movementBlendDampTime = 0.05f;
        [SerializeField] private float _weaponLayerTweenTime = 0.2f;

        private float _movementBlendWeight;
        private Tween _weaponLayerTween;

        private const string WeaponLayerName = "WeaponLayer";
        
        private void Update()
        {
            _animator.SetFloat("Movement", _movementBlendWeight, _movementBlendDampTime, Time.deltaTime);
        }

        internal void MovementBlend(bool p_isMoving)
        {
            _movementBlendWeight = p_isMoving ? 1 : 0;
        }

        internal void SetWeaponEquiped(bool p_isEquiped)
        {
            if (_weaponLayerTween != null)
            {
                _weaponLayerTween.Kill();
                _weaponLayerTween.onUpdate = null;
                _weaponLayerTween = null;
            }
            
            float currentWeight = _animator.GetLayerWeight(_animator.GetLayerIndex(WeaponLayerName));
            float targetWeight = p_isEquiped ? 1 : 0;
            _weaponLayerTween = DOTween.To(() => currentWeight, x => currentWeight = x, targetWeight, _weaponLayerTweenTime);
            _weaponLayerTween.onUpdate += () =>
            {
                _animator.SetLayerWeight(_animator.GetLayerIndex(WeaponLayerName), currentWeight);
            };
        }
    }
}