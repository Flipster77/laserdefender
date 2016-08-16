using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class PlayerShip : Ship {

	// How fast the ship moves
	public float speed;
	
	public int numShotsPerFire = 1;
	
	private HashSet<string> currentPowerups;

	// Use this for initialization
	void Start () {
		InitialiseVariables(true);
		currentPowerups = new HashSet<string>();
		SetupBoundaries();		
	}
	
	// Update is called once per frame
	void Update () {
		UpdateLocation();
		UpdateLaserFire();
	}
	
	private void UpdateLocation() {
	
		if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
			transform.position += Vector3.left * speed * Time.deltaTime;
			
		} else if (Input.GetKey(KeyCode.RightArrow)|| Input.GetKey(KeyCode.D)) {
			transform.position += Vector3.right * speed * Time.deltaTime;
		}
		
		/*if (Input.GetKey(KeyCode.UpArrow)) {
			transform.position += Vector3.up * speed * Time.deltaTime;
			
		} else if (Input.GetKey(KeyCode.DownArrow)) {
			transform.position += Vector3.down * speed * Time.deltaTime;
		}*/
		
		float newX = Mathf.Clamp(transform.position.x, xMin, xMax);
		float newY = Mathf.Clamp(transform.position.y, yMin, yMax);
		transform.position = new Vector3(newX, newY, transform.position.z);
	}
	
	private void UpdateLaserFire() {
		UpdateCanFire();
	
		if (Input.GetKey(KeyCode.Space) && canFire) {
			FireLasers();
		}
	}
	
	// Use the firing rate to determine if the player can fire
	private void UpdateCanFire() {
		if (timeSinceLastFired < 0) {
			timeSinceLastFired = 0;
		}
		
		timeSinceLastFired += Time.deltaTime;
		
		if (timeSinceLastFired > 1.0f / firingRate) {
			canFire = true;
		}
	}
	
	// Fire lasers
	private void FireLasers() {
	
		if (numShotsPerFire == 1) {
			Instantiate(basicLaser, transform.position, Quaternion.identity);
		}
		
		else if (numShotsPerFire == 2) {
			Instantiate(basicLaser, transform.position + Vector3.left*0.45f, Quaternion.identity);
			Instantiate(basicLaser, transform.position + Vector3.right*0.45f, Quaternion.identity);
		}
		timeSinceLastFired = 0f;
		canFire = false;
	}
	
	void OnTriggerEnter2D(Collider2D collider) {
		Laser laser = collider.gameObject.GetComponent<Laser>();
		EnemyShip enemyShip = collider.gameObject.GetComponent<EnemyShip>();
		Meteor meteor = collider.gameObject.GetComponent<Meteor>();
		
		// Laser hit
		if (laser != null) {
			currentHealth -= laser.Damage;
			laser.Hit();
			
			if (currentHealth <= 0) {
				PlayerDeath();
			} else {
				ShipDamaged();
			}
		} else if (enemyShip != null) { // Enemy ship collision
			PlayerDeath();
		} else if (meteor != null) { // Meteor collision
			PlayerDeath();
		}
	}
	
	private void PlayerDeath() {
		LevelManager levelManager = GameObject.Find ("LevelManager").GetComponent<LevelManager>();
		CreateExplosion();
		levelManager.GameOver();
		Destroy(gameObject);
	}
	
	public bool HasPowerup(string powerupTag) {
		return currentPowerups.Contains(powerupTag);
	}
	
	public void PowerupActive(string powerupTag, int effectLength, Action<PlayerShip> stopEffectMethod) {
		// Add powerup tag to collection
		currentPowerups.Add(powerupTag);
		
		// Stop the powerup after the effect finishes
		StartCoroutine(StopPowerupEffect(powerupTag, effectLength, stopEffectMethod));
	}
	
	private IEnumerator StopPowerupEffect(string powerupTag, int effectLength, Action<PlayerShip> stopEffectMethod) {
		yield return new WaitForSeconds(effectLength);
		stopEffectMethod(this);
		
		// Remove the powerup tag
		currentPowerups.Remove(powerupTag);
	}
}
