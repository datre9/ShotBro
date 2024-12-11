using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacleTrigger : MonoBehaviour
{
	public MovingObstacles movingObstacle;

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject.tag == "Player") {
			movingObstacle.startMoving = true;
		}
	}
}