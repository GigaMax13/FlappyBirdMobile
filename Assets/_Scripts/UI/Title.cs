using UnityEngine;

public class Title : MonoBehaviour {
  private string currentAnimaton;
  private Animator animator;

  private void Awake() {
    animator = GetComponent<Animator>();
  }

  private void OnEnable() {
    Actions.OnGameStart += OnGameStart;
  }

  private void OnDisable() {
    Actions.OnGameStart -= OnGameStart;
  }

  private void OnGameStart() {
    ChangeAnimationState("Hide");
  }

  private void ChangeAnimationState(string newAnimation) {
    if (currentAnimaton != newAnimation) {
      animator.Play(newAnimation);
      currentAnimaton = newAnimation;
    }
  }
}
