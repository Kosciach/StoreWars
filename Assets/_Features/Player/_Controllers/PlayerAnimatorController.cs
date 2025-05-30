using DG.Tweening;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Animations.Rigging;

namespace Kosciach.StoreWars.Player
{
    public class PlayerAnimatorController : PlayerControllerBase
    {
        [BoxGroup("References"), SerializeField] private Animator _animator;
        [Space(5), HorizontalLine(color: EColor.Gray)]
        [BoxGroup("References"), SerializeField] private Rig _weaponRig;
        [BoxGroup("References"), SerializeField] private MultiRotationConstraint _recoilConstraint;
        [BoxGroup("References"), SerializeField] private Transform _recoilTarget;

        [BoxGroup("Settings"), SerializeField] private float _movementBlendDampTime = 0.05f;
        [Space(5), HorizontalLine(color: EColor.Gray)]
        [BoxGroup("Settings"), SerializeField] private float _weaponRigTweenTime = 0.2f;
        [Space(5), HorizontalLine(color: EColor.Gray)]
        [BoxGroup("Settings"), SerializeField] private float _stunLayerTweenTime = 0.2f;
        
        private Tween _weaponRigTween;
        private Tween _recoilConstraintTween;
        private float _recoilTime;
        
        private Tween _stunLayerTween;
        
        
        //Movement
        internal void MovementBlend(bool p_isMoving)
        {
            int weight = p_isMoving ? 1 : 0;
            _animator.SetFloat("Movement", weight, _movementBlendDampTime, Time.deltaTime);
        }

        //Weapons
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

        //Stun
        internal void SetStunLayer(bool p_enabled)
        {
            if (_stunLayerTween != null)
            {
                _stunLayerTween.Kill();
                _stunLayerTween = null;
            }

            float targetWeight = p_enabled ? 1 : 0;
            float weight = _animator.GetLayerWeight(_animator.GetLayerIndex("StunLayer"));
            _stunLayerTween = DOTween.To(() => weight, x => weight = x, targetWeight, _stunLayerTweenTime);
            _stunLayerTween.OnUpdate(() =>
            {
                _animator.SetLayerWeight(_animator.GetLayerIndex("StunLayer"), weight);
            });
        }
        
        internal void Melee()
        {
            AnimatorStateInfo stateInfo = _animator.GetCurrentAnimatorStateInfo(0);
            if (!stateInfo.IsName("Melee"))
            {
                _animator.SetFloat("MeleeIndex", Random.Range(0, 2));
                _animator.SetTrigger("Melee");
            }
        }
    }
}