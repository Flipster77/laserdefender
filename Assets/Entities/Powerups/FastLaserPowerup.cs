using UnityEngine;
using System.Collections;

public class FastLaserPowerup : Powerup {

	public AudioClip collectSound;

	private const string powerupTag = "FastLaser";
	private const float firingRateMultiplier = 10f;
	private const float powerupTime = 10;

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

            // If the ship doesn't already have the powerup, start the effect
			if (!playerShip.HasPowerup(powerupTag)) {
				StartPowerupEffect(playerShip);
			}

            // Notify the ship that the powerup is active/extend the powerup time
            playerShip.PowerupReceived(powerupTag, powerupTime, StopPowerupEffect);

            // Play sound and destroy game object
            AudioSource.PlayClipAtPoint(collectSound, transform.position, 1.0f);
			Destroy(gameObject);
		}
	}
	
	public override void StartPowerupEffect(PlayerShip playerShip) {
		playerShip.firingRate = playerShip.firingRate * firingRateMultiplier;
		Debug.Log("Firing rate set to " + playerShip.firingRate);
	}
	
	
	public static void StopPowerupEffect(PlayerShip playerShip) {
		playerShip.firingRate = playerShip.firingRate / firingRateMultiplier;
		Debug.Log("Firing rate reset to " + playerShip.firingRate);
	}
	
	public override string GetPowerupTag() {
		return powerupTag;
	}
}
