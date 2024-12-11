using UnityEngine;

public class TouchObstacle : MonoBehaviour {
	private LevelManager levelManager;

	void Start() {
		levelManager = LevelManager.instance;
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject.tag == "Player") {
			levelManager.RespawnPlayer();
		}
	}
}