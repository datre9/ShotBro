using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTransitionCollision : MonoBehaviour
{
	public CameraManager cameraManager;

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject.tag == "Player") {
			cameraManager.IterateCameraLvl();
		}
	}
}