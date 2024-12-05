using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class PlayerMovement : MonoBehaviour {
	public float moveSpeed = 5f;
	public float recoilForce = 10;
	public float recoilDuration = 0.5f;

	private Vector2 moveInput;
	private float recoilTimer;
	private bool isFacingRight = true;

	private Rigidbody2D rb;
	private Animator animator;

	public LayerMask groundLayer;
	public Transform groundCheck;
	private bool isGrounded;


	private void Awake() {
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
	}

	private void Update() {
		isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
		HandleMovement();
		UpdateAnimator();
	}

	private void FixedUpdate() {
		if (recoilTimer <= 0) {
			Vector2 targetVelocity = new Vector2(moveInput.x * moveSpeed, rb.velocity.y);
			rb.velocity = targetVelocity;
		} else {
			recoilTimer -= Time.fixedDeltaTime;
		}
	}

	private void HandleMovement() {
		if (rb.velocity.x > 0.1f && !isFacingRight) {
			Flip();
		} else if (rb.velocity.x < -0.1f && isFacingRight) {
			Flip();
		}
	}

	private void Flip() {
		isFacingRight = !isFacingRight;
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}

	private void UpdateAnimator() {
		animator.SetBool(AnimationStrings.isMoving, Math.Abs(moveInput.x) > 0.1f);
	}

	public void OnMove(InputAction.CallbackContext context) {
		if (isGrounded) {
			moveInput = context.ReadValue<Vector2>();
		} else {
			moveInput = Vector2.zero;
		}
	}

	public void OnShoot(InputAction.CallbackContext context) {
		if (context.started && recoilTimer <= 0) {
			Vector2 shootDirection = context.ReadValue<Vector2>();
			if (shootDirection != Vector2.zero) {
				Shoot(shootDirection.normalized);
			}
		}
	}

	private void Shoot(Vector2 direction) {
		rb.velocity = -direction * recoilForce;
		recoilTimer = recoilDuration;

		//after shooting while holding a movement direction, player could still move with A and D
		moveInput = Vector2.zero;

		//TODO: shoot animations
		//scale animation size with recoilForce and recoilDuration?
		//animator.SetTrigger("shoot");
	}

	public void OnSwitch(InputAction.CallbackContext context) {
		float weapon = context.ReadValue<float>();
		if (context.started) {
			switch (weapon) {
				case 1f:
					//shotgun
					recoilForce = 10f;
					recoilDuration = 0.5f;
					break;
				case 2f:
					//assault rifle
					recoilForce = 5f;
					recoilDuration = 0.3f;
					break;
			}
		}
	}
}