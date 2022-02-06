using UnityEngine;

public class ScoreManager : MonoBehaviour {
  private int score;

  private void OnEnable() {
    Actions.OnGameStart += OnGameStart;
  }

  private void OnDisable() {
    Actions.OnGameStart -= OnGameStart;
  }

  private void OnGameStart() {
    score = 0;
  }

  private void OnTriggerEnter2D(Collider2D collider) {
    if (collider.tag == "Pipe") {
      Actions.OnPlayerScore?.Invoke(++score);
    }
  }
}
