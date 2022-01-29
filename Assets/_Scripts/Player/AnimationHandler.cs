using System;
using UnityEngine;

public class AnimationHandler : MonoBehaviour {
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
    ChangeAnimationState("Flapping");
  }

  private void OnTriggerEnter2D(Collider2D collider) {
    //SoundManager.PlaySound(AssetsManager.Sound.Die, .5f);
    Actions.OnGameOver?.Invoke();
    ChangeAnimationState("Dead");
    print("Colision with: " + collider);
  }

  private void ChangeAnimationState(string newAnimation) {
    if (currentAnimaton != newAnimation) {
      animator.Play(newAnimation);
      currentAnimaton = newAnimation;
    }
  }
}
