/***
 * This script will anchor a GameObject to a relative screen position.
 * This script is intended to be used with ViewportHandler.cs by Marcel Căşvan, available here: http://gamedev.stackexchange.com/a/89973/50623
 * It is also copied in this gist below.
 * 
 * Note: For performance reasons it's currently assumed that the game resolution will not change after the game starts.
 * You could not make this assumption by periodically calling UpdateAnchor() in the Update() function or a coroutine, but is left as an exercise to the reader.
 */
/* The MIT License (MIT)
Copyright (c) 2015, Eliot Lash
Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:
The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.
THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE. */
using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class GameObjectAnchor : MonoBehaviour {
  public enum AnchorType {
    BottomLeft,
    BottomCenter,
    BottomRight,
    MiddleLeft,
    MiddleCenter,
    MiddleRight,
    TopLeft,
    TopCenter,
    TopRight,
  };
  public AnchorType anchorType;
  public Vector3 anchorOffset;
  [SerializeField]
  private new Camera camera;
  private ViewportManager viewport;

  IEnumerator updateAnchorRoutine;

  private void Awake() {
    viewport = camera.GetComponent<ViewportManager>();
  }

  private void Start() {
    updateAnchorRoutine = UpdateAnchorAsync();
    StartCoroutine(updateAnchorRoutine);
  }

  IEnumerator UpdateAnchorAsync() {
    uint cameraWaitCycles = 0;

    while (viewport == null) {
      ++cameraWaitCycles;
      yield return new WaitForEndOfFrame();
    }

    if (cameraWaitCycles > 0) {
      print(string.Format("GameObjectAnchor found ViewportManager instance after waiting {0} frame(s). You might want to check if the camera {1} has a ViewportManager script.", cameraWaitCycles, camera.name));
    }

    UpdateAnchor();
    updateAnchorRoutine = null;
  }

  private void UpdateAnchor() {
    switch (anchorType) {
    case AnchorType.BottomLeft:
      SetAnchor(viewport.BottomLeft);
      break;
    case AnchorType.BottomCenter:
      SetAnchor(viewport.BottomCenter);
      break;
    case AnchorType.BottomRight:
      SetAnchor(viewport.BottomRight);
      break;
    case AnchorType.MiddleLeft:
      SetAnchor(viewport.MiddleLeft);
      break;
    case AnchorType.MiddleCenter:
      SetAnchor(viewport.MiddleCenter);
      break;
    case AnchorType.MiddleRight:
      SetAnchor(viewport.MiddleRight);
      break;
    case AnchorType.TopLeft:
      SetAnchor(viewport.TopLeft);
      break;
    case AnchorType.TopCenter:
      SetAnchor(viewport.TopCenter);
      break;
    case AnchorType.TopRight:
      SetAnchor(viewport.TopRight);
      break;
    }
  }

  private void SetAnchor(Vector3 anchor) {
    Vector3 newPos = anchor + anchorOffset;
    if (!transform.position.Equals(newPos)) {
      transform.position = newPos;
    }
  }

#if UNITY_EDITOR
  void Update() {
    print("Update");
    if (updateAnchorRoutine == null) {
      updateAnchorRoutine = UpdateAnchorAsync();
      StartCoroutine(updateAnchorRoutine);
    }
  }
#endif
}