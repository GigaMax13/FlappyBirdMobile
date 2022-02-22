using System.Collections.Generic;
using UnityEngine;

using ScoreUI = CustomTypes.ScoreUI;
using Player = CustomTypes.Player;
using Pipe = CustomTypes.Pipe;
using CustomTypes;

public class AssetsManager : MonoBehaviour {
  private static AssetsManager _instance;
  public static AssetsManager Instance {
    get {
      return _instance;
    }
  }

  #region PLAYER
  public Dictionary<int, Player.Asset> PlayerAssets { get; private set; }

  [SerializeField]
  private Player.Asset[] _playerAssets;
  #endregion

  #region PIPE
  public Dictionary<int, Pipe.Asset> PipeAssets { get; private set; }
  public Transform Pipe => pfPipe;

  [SerializeField]
  private Pipe.Asset[] _pipeAssets;

  [SerializeField]
  private Transform pfPipe;
  #endregion

  #region SCORE
  public ScoreUI.UI[] ScoreUI => _scoreUI;
  public ScoreUI.Asset[] ScoreUIAssets => _scoreUIAssets;

  [SerializeField]
  private ScoreUI.UI[] _scoreUI;
  [SerializeField]
  private ScoreUI.Asset[] _scoreUIAssets;
  #endregion

  private void Awake() {
    _instance = this;

    PlayerAssets = setAssetDictionary(_playerAssets);
    PipeAssets = setAssetDictionary(_pipeAssets);
  }

  private Dictionary<int, A> setAssetDictionary<A>(A[] arr)
    where A : IAsset {
    Dictionary<int, A> dic = new Dictionary<int, A>();

    foreach (A asset in arr) {
      dic.Add(asset.color, asset);
    }

    return dic;
  }
}
