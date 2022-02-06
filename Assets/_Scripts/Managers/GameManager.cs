using UnityEngine;
using Player = CustomTypes.Player;

public class GameManager : MonoBehaviour {
  private bool isGameStarted = false;
  private bool isGamePaused = true;
  private bool isGameOver = false;

  private int playerScore = 0;
  private int hightScore = 0;

  private bool hasInput = false;

  private void OnEnable() {
    Actions.OnPlayerScore += UpdatePlayerScore;
    Actions.OnGameOver += OnGameOver;
  }

  private void OnDisable() {
    Actions.OnPlayerScore -= UpdatePlayerScore;
    Actions.OnGameOver -= OnGameOver;
  }

  private void Update() {
    hasInput = (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0));

    if (!isGameStarted && hasInput) {
      OnGameStart();
    }

    if (isGameStarted && !isGameOver) {
      if (isGamePaused && hasInput) {
        ToggleGamePause();
      }
    }

    if (isGameOver && hasInput) {
      OnGameStart();
    }
  }

  private void UpdatePlayerScore(int newScore) {
    playerScore = newScore;
    print(string.Format("Score: {0}", playerScore));
  }

  private void OnGameStart() {
    isGameStarted = true;
    isGameOver = false;
    Actions.OnGameStart?.Invoke();
    Actions.OnPlayerScore?.Invoke(0);
    SetGamePause(false);
  }

  private void OnGameOver() {
    if (playerScore > hightScore) {
      hightScore = playerScore;
    }

    isGameStarted = false;
    isGameOver = true;
    SetGamePause(true);
  }

  private void SetGamePause(bool pauseValue) {
    isGamePaused = pauseValue;
    Actions.OnGamePause?.Invoke(isGamePaused);
  }

  private void ToggleGamePause() {
    isGamePaused = !isGamePaused;
    Actions.OnGamePause?.Invoke(isGamePaused);
  }

  private class GameState {
    public int playerScore { get; private set; }
    public int hightScore { get; private set; }

    virtual public GameState handleState() {
      return this;
    }
  }
}
