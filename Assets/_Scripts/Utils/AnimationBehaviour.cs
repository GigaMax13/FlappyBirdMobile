using UnityEngine;

public class AnimationBehaviour : MonoBehaviour {
  private string _currentAnimaton;

  protected Animator animator;

  protected virtual void Awake() {
    animator = GetComponent<Animator>();
  }

  protected void ChangeAnimationState(string newAnimation) {
    if (_currentAnimaton != newAnimation) {
      _currentAnimaton = newAnimation;
      animator.Play(newAnimation);
    }
  }
}
