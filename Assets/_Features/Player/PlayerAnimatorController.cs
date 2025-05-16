using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Animations.Rigging;

namespace Kosciach.StoreWars.Player
{
    public class PlayerAnimatorController : MonoBehaviour
    {
        [BoxGroup("References"), SerializeField] private Animator _animator;
        [Space(5), HorizontalLine(color: EColor.Gray)]
        [BoxGroup("References"), SerializeField] private Rig _weaponRig;
        [BoxGroup("References"), SerializeField] private MultiRotationConstraint _recoilConstraint;
        [BoxGroup("References"), SerializeField] private Transform _recoilTarget;

        [BoxGroup("Settings"), SerializeField] private float _movementBlendDampTime = 0.05f;
        [BoxGroup("Settings"), SerializeField] private float _weaponRigTweenTime = 0.2f;

        private float _movementBlendWeight;
        
        private Tween _weaponRigTween;
        private Tween _recoilConstraintTween;
        private float _recoilTime;
        
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
            if (_weaponRigTween != null)
            {
                _weaponRigTween.Kill();
                _weaponRigTween = null;
            }
            
            float targetWeight = p_isEquiped ? 1 : 0;
            _weaponRigTween = DOTween.To(() => _weaponRig.weight, x => _weaponRig.weight = x, targetWeight, _weaponRigTweenTime);
        }

        internal void SetRecoil(float p_recoil, float p_recoilTime)
        {
            Vector3 recoilTargetEuler = _recoilTarget.localEulerAngles;
            recoilTargetEuler.x = -(p_recoil/2f);
            recoilTargetEuler.z = p_recoil;
            _recoilTarget.localEulerAngles = recoilTargetEuler;
            
            _recoilTime = p_recoilTime;
        }
        
        internal void Shoot()
        {
            if (_recoilConstraintTween != null)
            {
                _recoilConstraintTween.Kill();
                _recoilConstraintTween = null;
            }

            _recoilConstraint.weight = 0;

            _recoilConstraintTween = DOTween.To(() => _recoilConstraint.weight, x => _recoilConstraint.weight = x, 1, _recoilTime/2f);
            _recoilConstraintTween.OnComplete(() =>
            {
                DOTween.To(() => _recoilConstraint.weight, x => _recoilConstraint.weight = x, 0, _recoilTime/2f);
            });
        }
    }
}