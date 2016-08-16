using UnityEngine;
using System.Collections;

public class EnemyShip : Ship {

	// Set these values in the unity inspector
	public int scoreValue;
	public float dropPowerupChance;
	public GameObject[] possiblePowerupDrops;

	// Use this for initialization
	void Start () {
		InitialiseVariables(false);
		SetupBoundaries();
	}
	
	// Update is called once per frame
	void Update () {
		UpdateLaserFire();
	}
	
	void OnTriggerEnter2D(Collider2D collider) {
		Laser laser = collider.gameObject.GetComponent<Laser>();
	
		if (laser != null) {
			currentHealth -= laser.Damage;
			laser.Hit();
			
			if (currentHealth <= 0) {
				EnemyDestroyed();
			} else {
				ShipDamaged();
			}
		}
	}
	
	private void UpdateLaserFire() {
		UpdateCanFire();
		if (canFire) {
			FireLasers();
		}
	}
	
	
	private void FireLasers() {
		Instantiate(basicLaser, transform.position, Quaternion.identity);
		
		timeSinceLastFired = 0f;
		canFire = false;
	}
	
	private void UpdateCanFire() {
		if (timeSinceLastFired < 0) {
			timeSinceLastFired = 0;
		}
		
		timeSinceLastFired += Time.deltaTime;
		
		float probability = Time.deltaTime * firingRate;
		
		// The time since last fired should at least be 1 / firingRate
		// use probability to randomise firing
		if (timeSinceLastFired > 1.0f / firingRate && Random.value < probability) {
			canFire = true;
		}
	}
	
	protected override void UpdateShipSprite() {
		int spriteIndex = maxHealth - currentHealth;
		
		if (shipSprites[spriteIndex] != null) {
			this.GetComponent<SpriteRenderer>().sprite = shipSprites[spriteIndex];
		} else {
			Debug.LogError("No sprite found at index " + spriteIndex);
		}
	}
	
	private void EnemyDestroyed() {
		ScoreKeeper scoreKeeper = GameObject.Find("Score").GetComponent<ScoreKeeper>();
		if (scoreKeeper != null) {
			scoreKeeper.AddPoints(scoreValue);
		}
		CreateExplosion();
		DropPowerupCheck();
		Destroy(gameObject);
	}
	
	private void DropPowerupCheck() {
		float randomDropNum = Random.Range(0f, 1f);
		
		if (randomDropNum <= dropPowerupChance) {
			int randomIndex = Random.Range (0, possiblePowerupDrops.Length);
			GameObject powerUp = possiblePowerupDrops[randomIndex];
			Instantiate(powerUp, this.transform.position, Quaternion.identity);
		}
	}
}
