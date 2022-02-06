using UnityEngine.InputSystem;
using UnityEngine;

public class MovementHandler : MonoBehaviour {
  private const float RISE_ANGLE_MULTIPLIER = 6;
  private const float FALL_ANGLE_MULTIPLIER = 3;

  [SerializeField]
  private float start = 1.5f;
  [SerializeField]
  private float gravityScale = 1.5f;
  [SerializeField]
  private float jumpForce = 200;

  private Rigidbody2D _rigidBody;
  private Transform _transform;

  private void Awake() {
    _rigidBody = GetComponent<Rigidbody2D>();
    _transform = GetComponent<Transform>();
  }

  private void FixedUpdate() {
    if (_rigidBody.gravityScale == 0) return;

    float velocity = _rigidBody.velocity.y;
    float angleMultiplier = velocity > 0 ? RISE_ANGLE_MULTIPLIER : FALL_ANGLE_MULTIPLIER;
    _transform.eulerAngles = new Vector3(0, 0, velocity * angleMultiplier);
  }

  private void OnEnable() {
    Actions.OnGameStart += OnGameStart;
    Actions.OnGameOver += OnGameOver;
  }

  private void OnDisable() {
    Actions.OnGameStart -= OnGameStart;
    Actions.OnGameOver -= OnGameOver;
  }

  private void OnGameOver() {
    _rigidBody.velocity = Vector2.zero;
    _rigidBody.gravityScale = 0;
  }

  private void OnGameStart() {
    _rigidBody.position = new Vector3(0, start, 0);
    _rigidBody.gravityScale = gravityScale;
  }

  public void Jump(InputAction.CallbackContext context) {
    if (_rigidBody.gravityScale == 0 || !context.started) return;

    _rigidBody.velocity = Vector2.zero;
    _rigidBody.AddForce(Vector2.up * jumpForce, ForceMode2D.Force);
  }
}
