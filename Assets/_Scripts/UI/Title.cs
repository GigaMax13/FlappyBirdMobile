public class Title : AnimationBehaviour {
  private void OnEnable() {
    Actions.OnGameStart += OnGameStart;
  }

  private void OnDisable() {
    Actions.OnGameStart -= OnGameStart;
  }

  private void OnGameStart() {
    ChangeAnimationState("Hide");
  }
}
