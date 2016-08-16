using UnityEngine;
using System.Collections;

public class EnemyFormation : MonoBehaviour {

	public GameObject enemyPrefab;
	public float width;
	public float height;
	public float spawnDelay = 0.5f;
	
	public float baseHorizontalSpeed = 5f;
	public float descentSpeed = 1f;
	public float formationSpeed = 0.1f;
	
	protected float xMin;
	protected float xMax;
	protected float yMin;
	protected float yMax;
	
	private bool goingLeft = true;
	
	private bool spawningComplete = false;

	// Use this for initialization
	void Start () {
		SetMinMaxBoundaries();
		SpawnEnemies();
	}
	
	// Update is called once per frame
	void Update () {
		UpdateFormation();
	}
	
	public void OnDrawGizmos() {
		Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
	}
	
	protected virtual void UpdateFormation() {
		MoveFormation();
		/*if (AllMembersDead()) {
			Destroy(gameObject);
		}*/
	}
	
	protected void SetMinMaxBoundaries() {
		float distanceToCamera = transform.position.z - Camera.main.transform.position.z; //Not needed for 2D game, but useful to know
		Vector3 bottomLeftPos = Camera.main.ViewportToWorldPoint(new Vector3(0,0,distanceToCamera));
		Vector3 topRightPos = Camera.main.ViewportToWorldPoint(new Vector3(1,1,distanceToCamera));
		
		xMin = bottomLeftPos.x + width/2;
		xMax = topRightPos.x - width/2;
		
		yMin = bottomLeftPos.y + height/2;
		yMax = topRightPos.y - height/2;
	}
	
	protected void SpawnEnemies() {
		/*foreach (Transform enemyPos in transform) {
			GameObject enemy = Instantiate(enemyPrefab, enemyPos.position, Quaternion.identity) as GameObject;
			enemy.transform.parent = enemyPos;
		}*/
		SpawnUntilFull();
		spawningComplete = true;
	}
	
	protected virtual void SpawnUntilFull() {
		Transform freePosition = NextFreePosition();
		if(freePosition != null) {
			SpawnEnemy(enemyPrefab, freePosition);
			Invoke("SpawnUntilFull", spawnDelay);
		}
	}
	
	protected void SpawnEnemy(GameObject enemyPrefab, Transform spawnPosition) {
		GameObject enemy = Instantiate(enemyPrefab, spawnPosition.position, Quaternion.identity) as GameObject;
		enemy.transform.parent = spawnPosition;
	}
	
	/// <summary>
	/// Gets the next free position in the formation.
	/// </summary>
	/// <returns>The free position.</returns>
	protected Transform NextFreePosition() {
		foreach (Transform childPosition in this.transform) {
			if (childPosition.childCount == 0) {
				return childPosition;
			}
		}
		
		return null;
	}
	
	protected virtual void MoveFormation() {
		if (goingLeft) {
			transform.position += (Vector3.left * baseHorizontalSpeed + Vector3.left * formationSpeed * 2f) * Time.deltaTime;
			
		} else {
			transform.position += (Vector3.right * baseHorizontalSpeed + Vector3.right * formationSpeed * 2f) * Time.deltaTime;
		}
		
		transform.position += (Vector3.down * descentSpeed * formationSpeed) * Time.deltaTime;
		
		if (transform.position.x <= xMin) {
			goingLeft = false;
		} else if (transform.position.x >= xMax) {
			goingLeft = true;
		}
		
		float newX = Mathf.Clamp(transform.position.x, xMin, xMax);
		float newY = Mathf.Clamp(transform.position.y, yMin, yMax);
		transform.position = new Vector3(newX, newY, transform.position.z);
	}
	
	public bool AllMembersDead() {
		if (!spawningComplete) {
			return false;
		}
	
		foreach (Transform childPosition in this.transform) {
			if (childPosition.childCount > 0) {
				//Debug.Log ("AllMembersDead returning false");
				return false;
			}
		}
		//Debug.Log ("AllMembersDead returning true");
		return true;
	}
}
