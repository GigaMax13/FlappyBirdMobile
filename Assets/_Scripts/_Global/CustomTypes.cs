using UnityEngine;
using System;

namespace CustomTypes {
  public interface IAsset {
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
    public class Asset : IAsset {
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
    public class Asset : IAsset {
      [SerializeField]
      private Color selectedColor;
      public Sprite sprite;
      public int color => (int)selectedColor;
    }
  }
}
