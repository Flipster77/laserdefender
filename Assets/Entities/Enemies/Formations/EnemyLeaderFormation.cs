using UnityEngine;
using System.Collections;

public class EnemyLeaderFormation : EnemyFormation {
	
	public GameObject enemyLeaderPrefab;
	
	private Transform leaderPosition;
	
	// Use this for initialization
	void Start () {
		leaderPosition = NextFreePosition();
	
		SetMinMaxBoundaries();
		SpawnEnemies();
	}
	
	// Update once per frame
	void Update () {
		UpdateFormation();
	}
	
	protected override void SpawnUntilFull() {
		Transform freePosition = NextFreePosition();
		if(freePosition != null) {
			if (freePosition != leaderPosition) {
				SpawnEnemy(enemyPrefab, freePosition);
			} else {
				SpawnEnemy(enemyLeaderPrefab, freePosition);
			}
			Invoke("SpawnUntilFull", spawnDelay);
		}
	}
}

