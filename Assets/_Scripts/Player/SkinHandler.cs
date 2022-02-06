using Player = CustomTypes.Player;
using UnityEngine;

public class SkinHandler : MonoBehaviour {
  private const string FLAPPING = "Flapping";
  private const string DEAD = "Dead";

  private AnimatorOverrideController overrideController;
  private Animator animator;

  private int lastSkinColor;

  private void Awake() {
    animator = GetComponent<Animator>();
    overrideController = new AnimatorOverrideController(animator.runtimeAnimatorController);
    animator.runtimeAnimatorController = overrideController;
  }

  private void OnEnable() {
    Actions.OnPlayerChangeSkin += OnPlayerChangeSkin;
  }

  private void OnDisable() {
    Actions.OnPlayerChangeSkin -= OnPlayerChangeSkin;
  }

  private void OnPlayerChangeSkin(Player.Color color) {
    int key = (int)color;

    if (lastSkinColor == key || !AssetsManager.Instance.PlayerAssets.ContainsKey(key)) return;

    lastSkinColor = key;

    AnimationClip flapping = AssetsManager.Instance.PlayerAssets[key].flapping;
    AnimationClip dead = AssetsManager.Instance.PlayerAssets[key].dead;

    overrideController[FLAPPING] = flapping;
    overrideController[DEAD] = dead;
  }
}
