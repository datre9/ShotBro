using UnityEngine;
using UnityEngine.Tilemaps;

public class MovingObstacles : MonoBehaviour {
	private LevelManager levelManager;
	public Tilemap t;
	private bool isFalling = false;

	void Start() {
		levelManager = LevelManager.instance;
	}

	private void Update() {
		if (Input.GetKeyDown(KeyCode.F)) {
			isFalling = true;
		}
		if (isFalling) {
			t.transform.position = new Vector3(
				t.transform.position.x,
				t.transform.position.y - 0.001f,
				t.transform.position.z);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject.tag == "Player") {
			levelManager.RespawnPlayer();
		}
	}
}