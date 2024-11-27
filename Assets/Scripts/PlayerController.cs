using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
	Vector2 moveInput;
	public float moveSpeed = 5f;

	private bool _isMoving = false;

	public bool isMoving {
		get {
			return _isMoving;
		}
		private set {
			_isMoving = value;
			animator.SetBool(AnimationStrings.isMoving, value);
		}
	}

	public bool _isFacingRight = true;

	public bool isFacingRight {
		get { return _isFacingRight; }
		private set {
			if (_isFacingRight != value) {
				transform.localScale *= new Vector2(-1, 1);
			}
			_isFacingRight = value;

		}
	}

	Rigidbody2D rb;
	Animator animator;

	private void Awake() {
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
	}

	void Start() {

	}

	void Update() {

	}

	void FixedUpdate() {
		rb.velocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);
	}

	public void OnMove(InputAction.CallbackContext context) {
		moveInput = context.ReadValue<Vector2>();

		isMoving = moveInput != Vector2.zero;

		SetFacingDirection(moveInput);
	}

	private void SetFacingDirection(Vector2 moveInput) {
		if (!isFacingRight && moveInput.x > 0) {
			isFacingRight = true;
		} else if (isFacingRight && moveInput.x < 0) {
			isFacingRight = false;
		}
	}
}