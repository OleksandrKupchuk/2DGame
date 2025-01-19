using UnityEngine;

public class AnimationController: MonoBehaviour {
    [SerializeField]
    private Animator _animator;

    public void PlayAnimation(string animationName) {
        _animator.Play(animationName);
    }

    public bool IsEndCurrentAnimation(int layer) {
        AnimatorStateInfo _stateInfo = _animator.GetCurrentAnimatorStateInfo(layer);

        if (_stateInfo.normalizedTime >= 1) {
            return true;
        }

        return false;
    }

    public bool IsEndCurrentAnimation(int layer, string currentAnimation) {
        AnimatorStateInfo _stateInfo = _animator.GetCurrentAnimatorStateInfo(layer);

        if (!_stateInfo.IsName(currentAnimation)) {
            return false;
        }
        if (_stateInfo.normalizedTime >= 1) {
            return true;
        }

        return false;
    }
}
