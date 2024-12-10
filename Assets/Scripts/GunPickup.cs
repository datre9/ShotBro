using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPickup : MonoBehaviour
{
	public GameObject self;

	private void OnTriggerEnter2D(Collider2D collision) {
		if (collision.gameObject.tag == "Player") {
			PlayerMovement playerMovement = collision.gameObject.GetComponent<PlayerMovement>();
			switch (self.name) {
				case "Rifle":
					playerMovement.hasRifle = true;
					break;
				case "Sniper":
					playerMovement.hasSniper = true;
					break;
			}
			self.SetActive(false);
		}
	}
}