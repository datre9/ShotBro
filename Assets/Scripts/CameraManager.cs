using Cinemachine;
using System;
using UnityEngine;

public class CameraManager : MonoBehaviour {
	public CinemachineVirtualCamera[] cameras;

	public CinemachineVirtualCamera startCam;
	public Collider2D t1;

	private CinemachineVirtualCamera currentCam;

	public static CameraManager instance { get; private set; }

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
	}

	void Update() {
		if (Input.GetKeyDown(KeyCode.K)) IterateCameraLvl();
	}

	public void IterateCameraLvl() {
		if (currentCam == cameras[cameras.Length - 1]) return;
		currentCam = cameras[Array.IndexOf(cameras, currentCam) + 1];

		currentCam.Priority = 20;

		foreach (var cam in cameras) {
			if (cam != currentCam) {
				cam.Priority = 10;
			}
		}
	}
}