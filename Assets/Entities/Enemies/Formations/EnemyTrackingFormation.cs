using UnityEngine;
using System.Collections;

public class EnemyTrackingFormation : EnemyFormation {

	private PlayerShip playerShip;	

	// Use this for initialization
	void Start () {	
		SetMinMaxBoundaries();
		playerShip = GameObject.FindObjectOfType<PlayerShip>();
		SpawnEnemies();
	}
	
	// Update once per frame
	void Update () {
		UpdateFormation();
	}
	
	protected override void MoveFormation() {
		float newX = this.transform.position.x;
		if (playerShip != null) {
			newX = Mathf.Clamp(playerShip.transform.position.x, xMin, xMax);
		}
		
		transform.position += Vector3.down * formationSpeed * descentSpeed * Time.deltaTime;
		float newY = Mathf.Clamp(this.transform.position.y, yMin, yMax);
		
		transform.position = new Vector3(newX, newY, transform.position.z);
	}
}
