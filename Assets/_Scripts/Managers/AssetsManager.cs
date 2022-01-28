using System.Collections.Generic;
using UnityEngine;

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

  [SerializeField]
  private Pipe.Asset[] _pipeAssets;
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
