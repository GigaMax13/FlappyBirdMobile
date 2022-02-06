using PipeColor = CustomTypes.Pipe.Color;
using Random = UnityEngine.Random;
using Object = UnityEngine.Object;
using System.Collections.Generic;
using UnityEngine;
using System;

/*
  15.5 ~ 8.8 Top

  10.2 Gap size

  -3.5 ~ 6.7 Gap position

  -12.3 ~ -8.8 Bottom
 */

public class LevelManager : MonoBehaviour {
  private const float MAX_PIPE_SPAWN_TIMER = 2;
  private const float MIN_PIPE_SPAWN_TIMER = 1.2f;
  private const float MIN_GAP_POS = -3.5f;
  private const float PIPE_X_POSITION = 4;
  private const float PIPE_HEIGHT = 8.8f;
  private const float PIPE_WIDTH = 1.4f;
  private const float MAX_GAP = 10.2f;
  private const float MIN_GAP = 3;
  private const float START_GAP = 6;

  private float pipeNextSpawnTimer;
  private float pipeSpawnTimer;
  private float pipeGapSize;
  private int pipesSpawned;
  private float speed = 3;
  private float lastGapY;
  private Vector2 resolution;

  private bool isGameRunning = false;

  private List<Pipe> pipes;

  private void OnEnable() {
    Actions.OnGameStart += OnGameStart;
    Actions.OnGamePause += OnGamePause;
  }

  private void OnDisable() {
    Actions.OnGameStart -= OnGameStart;
    Actions.OnGamePause -= OnGamePause;
  }

  private void Awake() {
    resolution = new Vector2(Screen.width, Screen.height);
    pipes = new List<Pipe>();
  }

  private void Update() {
    if (resolution.x != Screen.width || resolution.y != Screen.height) {
      resolution = new Vector2(Screen.width, Screen.height);
    }

    if (!isGameRunning) return;

    PipeSpawning();
    MovePipes();
  }

  private void OnGameStart() {
    DestroyPipes();

    pipes = new List<Pipe>();

    pipeNextSpawnTimer = MAX_PIPE_SPAWN_TIMER;
    pipeGapSize = START_GAP;
    isGameRunning = true;
    pipesSpawned = 0;
    lastGapY = (float)Math.PI;
  }

  private void OnGamePause(bool isPaused) {
    isGameRunning = !isPaused;
  }

  private void PipeSpawning() {
    pipeSpawnTimer -= Time.deltaTime;

    if (pipeSpawnTimer <= 0) {
      pipeSpawnTimer = (float)Math.Round(Random.Range(pipeNextSpawnTimer, MAX_PIPE_SPAWN_TIMER), 1);
      float pipeSpawnXPosition = PIPE_X_POSITION + PIPE_WIDTH;

      CreateGapPipes(pipeSpawnXPosition, pipeGapSize);
      pipesSpawned++;
      IncreaseDifficulty();
    }
  }

  private void IncreaseDifficulty() {
    if (pipeGapSize == MIN_GAP && pipeNextSpawnTimer == MIN_PIPE_SPAWN_TIMER) return;

    if (pipesSpawned % 5 == 0) {
      pipeNextSpawnTimer = Mathf.Max((float)Math.Round(pipeNextSpawnTimer - .1f, 1), MIN_PIPE_SPAWN_TIMER);
      pipeGapSize = Mathf.Max((float)Math.Round(pipeGapSize - .1f, 1), MIN_GAP);

      print(String.Format("Size: {0}, Timer: {1}", pipeGapSize, pipeNextSpawnTimer));
    }
  }

  private void MovePipes() {
    float pipeDestroyXPosition = (PIPE_X_POSITION + PIPE_WIDTH) * -1;

    for (int i = 0; i < pipes.Count; i++) {
      Pipe pipe = pipes[i];

      pipe.Move(speed);

      if (pipe.x <= pipeDestroyXPosition) {
        pipe.Destroy();
        pipes.Remove(pipe);
        i--;
      }
    }
  }

  private void CreateGapPipes(float x, float gapSize) {
    float gapY = (float)Math.Round(Random.Range(MIN_GAP_POS, MIN_GAP_POS + (MAX_GAP - gapSize)), 1);

    //if (lastGapY != (float)Math.PI) {
    //  float maxNextGap = (float)Math.Round((gapY + lastGapY) * .5f, 1);

    //  if (gapY > maxNextGap) {
    //    print(string.Format("Last: {0}, New: {1}, Max: {2}", lastGapY, gapY, maxNextGap));
    //  }

    //  gapY = MathF.Min(gapY, maxNextGap);
    //}

    pipes.Add(new Pipe(x, gapY + gapSize + PIPE_HEIGHT, false));
    pipes.Add(new Pipe(x, gapY - PIPE_HEIGHT));
    lastGapY = gapY;
  }

  private void DestroyPipes() {
    int i = pipes.Count - 1;

    while (i >= 0) {
      Pipe pipe = pipes[i];

      pipe.Destroy();
      pipes.Remove(pipe);

      i--;

    }
  }

  private class Pipe {
    private Transform _pipe;

    public Pipe(float x, float y, bool isGrounded = true, PipeColor color = PipeColor.Green) {
      _pipe = Instantiate(AssetsManager.Instance.Pipe);
      _pipe.position = new Vector3(x, y, 0f);

      if (!isGrounded) {
        _pipe.localScale = new Vector3(1, -1, 1);
      }
    }

    public void Move(float speed) {
      _pipe.position += new Vector3(-1, 0, 0) * speed * Time.deltaTime;
    }

    public float x => _pipe.position.x;

    public void Destroy() {
      Object.Destroy(_pipe.gameObject);
    }
  }
}
