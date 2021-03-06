using UnityEngine;

public class AnimationHandler : AnimationBehaviour {
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
    SoundManager.Instance.PlaySound(CustomTypes.Audio.Type.Die, .5f);
    Actions.OnGameOver?.Invoke();
    ChangeAnimationState("Dead");
  }
}
