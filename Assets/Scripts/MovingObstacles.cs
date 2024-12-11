using UnityEngine;
using UnityEngine.Tilemaps;

public class MovingObstacles : MonoBehaviour {
	private LevelManager levelManager;
	public Tilemap t;
	public bool startMoving = false;

	public float xSpeed;
	public float ySpeed;
	public float zSpeed;


	void Start() {
		levelManager = LevelManager.instance;
	}

	private void Update() {
		if (Input.GetKeyDown(KeyCode.F)) {
			startMoving = true;
		}
		if (startMoving) {
			t.transform.position = new Vector3(
				t.transform.position.x + xSpeed,
				t.transform.position.y + ySpeed,
				t.transform.position.z + zSpeed);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject.tag == "Player") {
			levelManager.RespawnPlayer();
		}
	}
}