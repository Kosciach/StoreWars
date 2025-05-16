using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Animations.Rigging;

namespace Kosciach.StoreWars.Player
{
    public class PlayerAnimatorController : MonoBehaviour
    {
        [BoxGroup("References"), SerializeField] private Animator _animator;
        [Space(5), HorizontalLine(color: EColor.Gray)]
        [BoxGroup("References"), SerializeField] private Rig _recoilRig;
        [BoxGroup("References"), SerializeField] private Transform _recoilTarget;

        [BoxGroup("Settings"), SerializeField] private float _movementBlendDampTime = 0.05f;
        [BoxGroup("Settings"), SerializeField] private float _weaponLayerTweenTime = 0.2f;

        private float _movementBlendWeight;
        private Tween _weaponLayerTween;

        private float _recoilTime;
        private Tween _recoilWeightTween;

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

        internal void SetRecoil(float p_recoil, float p_recoilTime)
        {
            _recoilTarget.localEulerAngles = new Vector3(-p_recoil, 0, 0);
            _recoilTime = p_recoilTime;
        }
        
        internal void Shoot()
        {
            if (_recoilWeightTween != null)
            {
                _recoilWeightTween.Kill();
                _recoilWeightTween = null;
            }

            _recoilRig.weight = 0;

            _recoilWeightTween = DOTween.To(() => _recoilRig.weight, x => _recoilRig.weight = x, 1, _recoilTime/2f);
            _recoilWeightTween.OnComplete(() =>
            {
                DOTween.To(() => _recoilRig.weight, x => _recoilRig.weight = x, 0, _recoilTime/2f);
            });
        }
    }
}