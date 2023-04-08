using UnityEngine;

public class LogicEnemy {

    public void AddEventForFrameOfAnimation(AnimationClip animation, AnimationEvent animationEvent, int frameRateAnimation, string nameMethod) {
        float _playingAnimationTime = frameRateAnimation / animation.frameRate;
        animationEvent.time = _playingAnimationTime;
        animationEvent.functionName = nameMethod;

        animation.AddEvent(animationEvent);
    }

    public void AddEventToEndOfAnimation(AnimationClip animation, AnimationEvent animationEvent, string nameMethod) {
        float _playingAnimationTime = animation.length;
        animationEvent.time = _playingAnimationTime;
        animationEvent.functionName = nameMethod;

        animation.AddEvent(animationEvent);
    }

    public void EnableCollider(Collider2D collider2D) {
        collider2D.enabled = true;
    }

    public void DisableCollider(Collider2D collider2D) {
        collider2D.enabled = false;
    }
}
