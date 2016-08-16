using UnityEngine;
using System.Collections;

public class MeteorSpawner : MonoBehaviour {

	public GameObject[] meteorPrefabs;
	public GameObject meteorWarning;
	public GameObject canvas;
	
	private bool meteorStormFinished;

	private float xMin;
	private float xMax;
	private float yMin;
	private float yMax;

	// Use this for initialization
	void Start () {
		meteorStormFinished = true;
		SetupBoundaries();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	protected void SetupBoundaries() {
		// Set the boundaries
		float distanceToCamera = transform.position.z - Camera.main.transform.position.z; //Not needed for 2D game, but useful to know
		Vector3 bottomLeftPos = Camera.main.ViewportToWorldPoint(new Vector3(0,0,distanceToCamera));
		Vector3 topRightPos = Camera.main.ViewportToWorldPoint(new Vector3(1,1,distanceToCamera));
		
		xMin = bottomLeftPos.x;
		xMax = topRightPos.x;
		
		yMin = bottomLeftPos.y;
		yMax = topRightPos.y;
	}
	
	public void StartMeteorStorm() {
		meteorStormFinished = false;
		StartCoroutine(DisplayMeteorWarning());
	}
	
	public bool MeteorStormFinished() {
		return meteorStormFinished;
	}
	
	protected IEnumerator DisplayMeteorWarning() {
		GameObject warningText = Instantiate(meteorWarning, meteorWarning.transform.position, Quaternion.identity) as GameObject;
		//GameObject canvas
		warningText.transform.SetParent(canvas.transform, false);
		yield return new WaitForSeconds(3);
		Destroy(warningText);
		
		// Create the meteors
		StartCoroutine(CreateMeteorStorm(6));
	}
	
	protected IEnumerator CreateMeteorStorm(int num) {
		Debug.Log("Creating meteor storm");
		for (int i = 0; i < num; i++) {
			SpawnMeteor();
			yield return new WaitForSeconds(1);
		}
		meteorStormFinished = true;
	}
	
	private void SpawnMeteor() {
	
		int meteorIndex = Random.Range(0, meteorPrefabs.Length);
		GameObject meteorPrefab = meteorPrefabs[meteorIndex];
	
		float xPos = Random.Range(xMin, xMax);
		float yPos = yMax + meteorPrefab.GetComponent<SpriteRenderer>().bounds.size.y;
		Vector3 spawnPosition = new Vector3(xPos, yPos, 0);
		GameObject meteor = Instantiate(meteorPrefab, spawnPosition, Quaternion.identity) as GameObject;
		meteor.transform.parent = this.gameObject.transform;
	}
}
