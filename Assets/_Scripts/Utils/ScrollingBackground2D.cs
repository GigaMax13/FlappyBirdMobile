using System;
using UnityEngine;
using UnityEngine.UI;

public class ScrollingBackground2D : MonoBehaviour {
  [SerializeField]
  private Vector2 speed;
  private RawImage img;

  private bool isScrollingEnable = true;

  private void OnEnable() {
    Actions.OnGameStart += OnGameStart;
    Actions.OnGameOver += OnGameOver;
  }

  private void OnDisable() {
    Actions.OnGameStart -= OnGameStart;
    Actions.OnGameOver -= OnGameOver;
  }

  private void Awake() {
    img = GetComponent<RawImage>();
  }

  private void Update() {
    if (!isScrollingEnable) return;

    img.uvRect = new Rect(img.uvRect.position + speed * Time.deltaTime, img.uvRect.size);
  }

  private void OnGameStart() {
    isScrollingEnable = true;
  }

  private void OnGameOver() {
    isScrollingEnable = false;
  }
}
