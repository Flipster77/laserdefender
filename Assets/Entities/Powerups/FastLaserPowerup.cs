using UnityEngine;
using System.Collections;

public class FastLaserPowerup : Powerup {

	public AudioClip collectSound;

	public const string powerupTag = "FastLaser";
	private const float firingRateMultiplier = 10f;
	private const int powerupTime = 10;

	// Use this for initialization
	void Start () {
		StartMethod();
	}
	
	// Update is called once per frame
	void Update () {
		UpdateMethod();
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		PlayerShip playerShip = other.GetComponent<PlayerShip>();
		
		if (playerShip != null) {
			if (!playerShip.HasPowerup(powerupTag)) {
				ActivatePowerup(playerShip);
			}
			
			// Play sound and destroy game object
			AudioSource.PlayClipAtPoint(collectSound, transform.position, 1.0f);
			Destroy(gameObject);
		}
	}
	
	public override void ActivatePowerup(PlayerShip playerShip) {
		playerShip.firingRate = playerShip.firingRate * firingRateMultiplier;
		Debug.Log("Firing rate set to " + playerShip.firingRate);
		playerShip.PowerupActive(powerupTag, powerupTime, StopPowerupEffect);
	}
	
	
	public static void StopPowerupEffect(PlayerShip playerShip) {
		playerShip.firingRate = playerShip.firingRate / firingRateMultiplier;
		Debug.Log("Firing rate reset to " + playerShip.firingRate);
	}
	
	public override string GetPowerupTag() {
		return powerupTag;
	}
}
