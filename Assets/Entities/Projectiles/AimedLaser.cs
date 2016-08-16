using UnityEngine;
using System.Collections;

public class AimedLaser : Laser {

	private PlayerShip playerShip;

	// Use this for initialization
	void Start () {
		playerShip = GameObject.FindObjectOfType<PlayerShip>();
		if (playerShip != null) {
			direction = playerShip.transform.position - this.transform.position;
			direction.Normalize();
			AimLaserAtTarget();
		}
		StartMethod();
	}
	
	// Update is called once per frame
	void Update () {
		UpdateMethod();
	}
	
	protected void AimLaserAtTarget() {
		float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		angle += 90f;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}
}
