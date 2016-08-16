using UnityEngine;
using System.Collections;

public abstract class Ship : MonoBehaviour {
	
	public string shipName;
	
	// Health variables
	public int maxHealth;
	protected int currentHealth;
	protected bool damageTintRunning;
	
	public Sprite[] shipSprites;
	public AudioClip hitSound;
	public AudioClip deathSound;
	public GameObject deathExplosion;
	
	// Laser variables
	public GameObject basicLaser;
	public float firingRate;
	protected float timeSinceLastFired;
	protected bool canFire;
	
	// The min and max positions on the screen for the ship
	protected float xMin;
	protected float xMax;
	protected float yMin;
	protected float yMax;
	
	protected void InitialiseVariables(bool canFireAtStart) {
		currentHealth = maxHealth;
		damageTintRunning = false;
		canFire = canFireAtStart;
		if (!canFireAtStart) {
			timeSinceLastFired = 0f;
		}
	}
	
	protected void SetupBoundaries() {
		// Set the boundaries
		float distanceToCamera = transform.position.z - Camera.main.transform.position.z; //Not needed for 2D game, but useful to know
		Vector3 bottomLeftPos = Camera.main.ViewportToWorldPoint(new Vector3(0,0,distanceToCamera));
		Vector3 topRightPos = Camera.main.ViewportToWorldPoint(new Vector3(1,1,distanceToCamera));
		
		Vector3 spriteSize = gameObject.GetComponent<SpriteRenderer>().bounds.size;
		
		xMin = bottomLeftPos.x + spriteSize.x/2;
		xMax = topRightPos.x - spriteSize.x/2;
		
		yMin = bottomLeftPos.y + spriteSize.y/2;
		yMax = topRightPos.y - spriteSize.y/2;
	}
	
	protected void ShipDamaged() {
		AudioSource.PlayClipAtPoint(hitSound, transform.position, 1.0f);
		
		// Stop the current damage tinting and start a new one
		if (damageTintRunning) {
			StopCoroutine("DamageTint");
		}
		StartCoroutine("DamageTint");
		
		// Update the ship sprite to represent damage
		UpdateShipSprite();
	}
	
	protected IEnumerator DamageTint() {
		damageTintRunning = true;
		for (float f = 0f; f < 1; f += 0.05f) {
			Color tempColour = GetComponent<Renderer>().material.color;
			tempColour.g = f;
			tempColour.b = f;
			GetComponent<Renderer>().material.color = tempColour;
			yield return null;
		}
		damageTintRunning = false;
	}
	
	protected virtual void UpdateShipSprite() {
		int spriteIndex = (maxHealth - currentHealth)/2;
		
		if (shipSprites[spriteIndex] != null) {
			this.GetComponent<SpriteRenderer>().sprite = shipSprites[spriteIndex];
		} else {
			Debug.LogError("No sprite found at index " + spriteIndex);
		}
	}
	
	protected void CreateExplosion() {
		// Play explosion sound
		AudioSource.PlayClipAtPoint(deathSound, transform.position, 0.7f);
		
		// Create explosion effect
		GameObject explosion = Instantiate(deathExplosion, transform.position, Quaternion.identity) as GameObject;
		float explosionLifetime = explosion.GetComponent<ParticleSystem>().duration + explosion.GetComponent<ParticleSystem>().startLifetime;
		Destroy(explosion, explosionLifetime);
	}
}
