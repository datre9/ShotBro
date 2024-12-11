using Cinemachine;
using System;
using UnityEngine;

public class LevelManager : MonoBehaviour {
	public GameObject player;

	[Header("Camera settings")]
	public CinemachineVirtualCamera[] cameras;
	public CinemachineVirtualCamera startCam;
	private CinemachineVirtualCamera currentCam;

	[Header("Respawn settings")]
	public GameObject[] respawns;
	public GameObject startRespawn;
	private GameObject currentRespawn;

	public static LevelManager instance { get; private set; }

	private void Awake() {
		if (instance != null && instance != this) {
			Destroy(gameObject);
			return;
		}
		instance = this;
		DontDestroyOnLoad(gameObject);
	}

	void Start() {
		currentCam = startCam;

		foreach (var cam in cameras) {
			cam.Priority = 10;
		}
		currentCam.Priority = 20;

		currentRespawn = startRespawn;
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.R)) {
			RespawnPlayer();
		}
	}

	public void IterateLevel() {
		//move camera
		if (currentCam != cameras[cameras.Length - 1]) {
			currentCam = cameras[Array.IndexOf(cameras, currentCam) + 1];

			currentCam.Priority = 20;

			foreach (var cam in cameras) {
				if (cam != currentCam) {
					cam.Priority = 10;
				}
			}
		}

		//move respawn
		if (currentRespawn != respawns[respawns.Length - 1]) {
			currentRespawn = respawns[Array.IndexOf(respawns, currentRespawn) + 1];
		}
	}

	public void RespawnPlayer() {
		player.transform.position = new Vector3(
			currentRespawn.transform.position.x,
			currentRespawn.transform.position.y,
			currentRespawn.transform.position.z);
	}
}