using UnityEngine;
using System;

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
      SoundManager.Instance.PlaySound(CustomTypes.Audio.Type.Point, .1f);
      Actions.OnPlayerScore?.Invoke(score);
    }
  }

  private void ShowScore() {
    string scoreValue = score.ToString();
    int length = scoreValue.Length;

    print(string.Format("Score: {0}, Length: {1}", scoreValue, length));

    AssetsManager.Instance.ScoreUI[length - 1].SetValue(scoreValue);

    for (int i = 0; i < AssetsManager.Instance.ScoreUI.Length; i++) {
      AssetsManager.Instance.ScoreUI[i].SetActive(i == length - 1);
    }
  }
}
