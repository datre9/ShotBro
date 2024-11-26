using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
	Vector2 moveInput;
	public float moveSpeed = 5f;

	Rigidbody2D rb;

	private void Awake() {
		rb = GetComponent<Rigidbody2D>();
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
	}
}