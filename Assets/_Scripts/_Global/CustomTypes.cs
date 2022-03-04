using UnityEngine.UI;
using UnityEngine;
using System;

namespace CustomTypes {
  public interface IAssetColor {
    private enum Color { }
    public int color { get; }
  }

  namespace Player {
    public enum Color {
      Yellow,
      Blue,
      Red
    }

    [Serializable]
    public class Asset : IAssetColor {
      [SerializeField]
      private Color selectedColor;
      public AnimationClip flapping;
      public AnimationClip dead;
      public int color => (int)selectedColor;
    }
  }

  namespace Pipe {
    public enum Color {
      Green,
      Red
    }

    [Serializable]
    public class Asset : IAssetColor {
      [SerializeField]
      private Color selectedColor;
      public Sprite sprite;
      public int color => (int)selectedColor;
    }
  }

  namespace ScoreUI {
    [Serializable]
    public class Asset {
      public Sprite sprite;
    }

    [Serializable]
    public class UI {
      [SerializeField]
      private RectTransform wrapper;
      [SerializeField]
      private Image hundred;
      [SerializeField]
      private Image ten;
      [SerializeField]
      private Image unit;

      public void SetActive(bool active) {
        wrapper.gameObject.SetActive(active);
      }

      public void SetValue(string value) {
        int length = value.Length;

        if (length == 1) {
          unit.sprite = AssetsManager.Instance.ScoreUIAssets[Int32.Parse(value)].sprite;
        } else if (length == 2) {
          ten.sprite = AssetsManager.Instance.ScoreUIAssets[Int32.Parse(value[0].ToString())].sprite;
          unit.sprite = AssetsManager.Instance.ScoreUIAssets[Int32.Parse(value[1].ToString())].sprite;
        } else {
          hundred.sprite = AssetsManager.Instance.ScoreUIAssets[Int32.Parse(value[0].ToString())].sprite;
          ten.sprite = AssetsManager.Instance.ScoreUIAssets[Int32.Parse(value[1].ToString())].sprite;
          unit.sprite = AssetsManager.Instance.ScoreUIAssets[Int32.Parse(value[2].ToString())].sprite;
        }
      }
    }
  }

  namespace Audio {
    public enum Type {
      Flappy,
      Point,
      Die
    }

    [Serializable]
    public class SoundAudioClip {
      public AudioClip audioClip;
      public Type type;
    }
  }
}
