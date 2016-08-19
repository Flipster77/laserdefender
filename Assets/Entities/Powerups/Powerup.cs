using UnityEngine;
using System.Collections;

public abstract class Powerup : MonoBehaviour {

	private float dropSpeed = 5f;
	
	private float xMin;
	private float xMax;
	private float yMin;
	private float yMax;
	
	// Use this for initialization
	void Start () {
		StartMethod();
	}
	
	// Update is called once per frame
	void Update () {
		UpdateMethod();
	}
	
	protected void StartMethod() {
		SetupBoundaries();
		GetComponent<Rigidbody2D>().velocity = Vector3.down * dropSpeed;
		//AudioSource.PlayClipAtPoint(fireSound, transform.position, 1.0f);
	}
	
	protected void UpdateMethod() {
		if (transform.position.x < xMin || transform.position.x > xMax) {
			Destroy(this.gameObject);
		}
		
		if (transform.position.y < yMin || transform.position.y > yMax) {
			Destroy(this.gameObject);
		}
	}
	
	protected void SetupBoundaries() {
		// Set the boundaries
		float distanceToCamera = transform.position.z - Camera.main.transform.position.z; //Not needed for 2D game, but useful to know
		Vector3 bottomLeftPos = Camera.main.ViewportToWorldPoint(new Vector3(0,0,distanceToCamera));
		Vector3 topRightPos = Camera.main.ViewportToWorldPoint(new Vector3(1,1,distanceToCamera));
		
		Vector3 spriteSize = gameObject.GetComponent<SpriteRenderer>().bounds.size;
		
		xMin = bottomLeftPos.x - spriteSize.x;
		xMax = topRightPos.x + spriteSize.x;
		yMax = topRightPos.y + spriteSize.y;
		yMin = bottomLeftPos.y - spriteSize.y;
	}
	
	public abstract void StartPowerupEffect(PlayerShip playerShip);
	
	public abstract string GetPowerupTag();
}
