using UnityEngine;

public class LoopingTexture : MonoBehaviour {
  private Material material;

  [SerializeField]
  private Vector2 speed = new Vector2(-.5f, 0);

  private void Awake() {
    material = GetComponent<Renderer>().material;
  }

  private void Update() {
    material.SetTextureOffset("_MainTex", new Vector2(speed.x * Time.time, speed.y * Time.time));
  }
}
