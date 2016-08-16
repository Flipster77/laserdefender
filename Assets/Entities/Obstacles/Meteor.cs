using UnityEngine;
using System.Collections;

public class Meteor : MonoBehaviour {

	public AudioClip laserHitSound;
	
	public Vector3 direction;
	public float speed;
	
	private float leftmostX;
	private float rightmostX;
	
	private float xMin;
	private float xMax;
	private float yMin;
	
	// Use this for initialization
	void Start () {
		StartMethod();
	}
	
	// Update is called once per frame
	void Update () {
		UpdateMethod();
	}
	
	void OnTriggerEnter2D(Collider2D collider) {
		Laser laser = collider.gameObject.GetComponent<Laser>();
		
		if (laser != null) {
			laser.Hit();
			
			// Play laser hit sound
			AudioSource.PlayClipAtPoint(laserHitSound, transform.position, 1.0f);
		}
	}
	
	protected void StartMethod() {
		SetupBoundaries();
		RandomRotation();
		SetVelocity();
	}
	
	protected void UpdateMethod() {
		if (transform.position.x < xMin || transform.position.x > xMax) {
			Destroy(this.gameObject);
		}
		
		if (transform.position.y < yMin) {
			Destroy(this.gameObject);
		}
	}
	
	private void RandomRotation() {
		float randomAngle = Random.Range(0f, 360f);
		Vector3 rotation = new Vector3(0f, 0f, randomAngle);
		gameObject.transform.Rotate(rotation);
	}
	
	private void SetVelocity() {
		float xMinRange = leftmostX - this.transform.position.x;
		float xMaxRange = rightmostX - this.transform.position.x;
		
		float xDir = Random.Range(xMinRange, xMaxRange);
		direction = new Vector3(xDir, yMin*2f, 0f);
		direction.Normalize();
		GetComponent<Rigidbody2D>().velocity = direction * speed;
	}
	
	protected void SetupBoundaries() {
		// Set the boundaries
		float distanceToCamera = transform.position.z - Camera.main.transform.position.z; //Not needed for 2D game, but useful to know
		Vector3 bottomLeftPos = Camera.main.ViewportToWorldPoint(new Vector3(0,0,distanceToCamera));
		Vector3 topRightPos = Camera.main.ViewportToWorldPoint(new Vector3(1,1,distanceToCamera));
		
		Vector3 spriteSize = gameObject.GetComponent<SpriteRenderer>().bounds.size;
		
		leftmostX = bottomLeftPos.x;
		rightmostX = topRightPos.x;
		
		xMin = bottomLeftPos.x - spriteSize.x;
		xMax = topRightPos.x + spriteSize.x;
		
		yMin = bottomLeftPos.y - spriteSize.y;
	}
}
