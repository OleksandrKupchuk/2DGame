using UnityEngine;

public class PlayerWeaponController : MonoBehaviour {
    private AttachingEventToAnimation _attachingEventToAnimation;
    private AnimationEvent _attackEvent = new AnimationEvent();

    [SerializeField]
    private Weapon _weapon;
    [SerializeField]
    protected int _frameRateInAttackAnimationForEnableCollider;
    [SerializeField]
    protected int _frameRateInAttackAnimationForDisableCollider;
    [SerializeField]
    protected AnimationClip _attackAnimation;

    public void Init() {
        _attachingEventToAnimation = new AttachingEventToAnimation();
        AddEnableSwordColliderEventForAttackAnimation();
        AddDisableSwordColliderEventForAttackAnimation();
        DisableWeaponCollider();
    }

    private void AddEnableSwordColliderEventForAttackAnimation() {
        _attachingEventToAnimation.AddEventForFrameOfAnimation(_attackAnimation, _attackEvent, _frameRateInAttackAnimationForEnableCollider, nameof(EnableWeaponCollider));
    }

    private void AddDisableSwordColliderEventForAttackAnimation() {
        _attachingEventToAnimation.AddEventForFrameOfAnimation(_attackAnimation, _attackEvent, _frameRateInAttackAnimationForDisableCollider, nameof(DisableWeaponCollider));
    }

    private void EnableWeaponCollider() {
        _weapon.BoxCollider2D.enabled = true;
    }

    private void DisableWeaponCollider() {
        _weapon.BoxCollider2D.enabled = false;
    }
}
