using Player = CustomTypes.Player;
using System;

public static class Actions {
  public static Action<Player.Color> OnPlayerChangeSkin;

  public static Action<int> OnPlayerScore;
  public static Action<bool> OnGamePause;
  public static Action OnGameStart;
  public static Action OnGameOver;
}