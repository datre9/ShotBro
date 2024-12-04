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
	private Rigidbody2D rb;
	private Animator animator;

	private bool isFacingRight = true;

	private void Awake() {
		rb = GetComponent<Rigidbody2D>();
		animator = GetComponent<Animator>();
	}

	private void Update() {
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
		moveInput = context.ReadValue<Vector2>();
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

		//TODO: shoot animations
		//animator.SetTrigger("shoot");
	}

	public void OnSwitch(InputAction.CallbackContext context) {
		float weapon = context.ReadValue<float>();
		if (context.started) {
			switch(weapon) {
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