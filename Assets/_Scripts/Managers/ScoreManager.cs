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
    ShowScore();
  }

  private void OnTriggerEnter2D(Collider2D collider) {
    if (collider.tag == "Pipe") {
      score++;

      ShowScore();
      SoundManager.Instance.PlaySound(CustomTypes.Audio.Type.Point, .15f);
      Actions.OnPlayerScore?.Invoke(score);
    }
  }

  private void ShowScore() {
    string scoreValue = score.ToString();
    int length = scoreValue.Length;

    AssetsManager.Instance.ScoreUI[length - 1].SetValue(scoreValue);

    for (int i = 0; i < length; i++) {
      AssetsManager.Instance.ScoreUI[i].SetActive(i == length - 1);
    }
  }
}
