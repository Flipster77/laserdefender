using UnityEngine;
using System.Collections;

public class Laser : MonoBehaviour {

	public AudioClip fireSound;
	public GameObject laserHitEffect;

	public Vector3 direction;
	public float speed;
	public int Damage { get {return damage;} private set {damage = value;} }
	[SerializeField]
	private int damage = 1;
	
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
		GetComponent<Rigidbody2D>().velocity = direction * speed;
		AudioSource.PlayClipAtPoint(fireSound, transform.position, 1.0f);
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
		
		yMin = bottomLeftPos.y - spriteSize.y;
		yMax = topRightPos.y + spriteSize.y;
	}
	
	public void Hit() {
		// Create laser hit effect
		GameObject laserHit = Instantiate(laserHitEffect, transform.position, Quaternion.identity) as GameObject;
		float effectLifetime = laserHit.GetComponent<ParticleSystem>().duration + laserHit.GetComponent<ParticleSystem>().startLifetime;
		Destroy(laserHit, effectLifetime);
	
		Destroy(gameObject);
	}
}
