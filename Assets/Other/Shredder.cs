using UnityEngine;
using System.Collections;

public class Shredder : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnTriggerEnter2D(Collider2D collider) {
		Debug.Log("Shredder destroyed object: " + collider.gameObject.ToString());
		Destroy(collider.gameObject);
	}
	
	public void OnDrawGizmos() {
		Vector3 bounds = gameObject.GetComponent<BoxCollider2D>().size;
		Gizmos.color = Color.green;
		Gizmos.DrawWireCube(transform.position, new Vector3(bounds.x, bounds.y));
	}
}
