using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyWave : Wave {

	private IList<GameObject> enemyFormations;
	private IList<EnemyFormation> enemyFormationInstances;
	
	private float waveSpeed;
	
	public EnemyWave() {
		enemyFormations = new List<GameObject>();
		enemyFormationInstances = new List<EnemyFormation>();
		waveSpeed = 0.1f;
	}
	
	public void StartWave() {
		
		foreach (GameObject formation in enemyFormations) { //Enemies being instantiated are not the ones in the list
			GameObject temp = MonoBehaviour.Instantiate(formation, new Vector3(0f, 3.5f, 0f), Quaternion.identity) as GameObject;
			EnemyFormation tempFormation = temp.GetComponent<EnemyFormation>();
			tempFormation.formationSpeed = waveSpeed;
			enemyFormationInstances.Add(tempFormation);
		}
	}
	
	public bool WaveIsComplete() {
		if (enemyFormationInstances.Count == 0) {
			//Debug.Log("No enemy formations, returning false");
			return false;
		}
	
		foreach (EnemyFormation enemyFormation in enemyFormationInstances) {
			//Debug.Log("Checking enemy formation: " + enemyFormation.GetInstanceID());
			if (!enemyFormation.AllMembersDead()) {
				return false;
			}
		}
		
		//Debug.Log("All enemies dead, clearing the list");
		// All enemies are dead, clear the list
		foreach (EnemyFormation enemyFormation in enemyFormationInstances) {
			GameObject.Destroy(enemyFormation.gameObject);
		}
		enemyFormationInstances.Clear();
		
		return true;
	}
	
	public void AddEnemyFormation(GameObject formation) {
		enemyFormations.Add(formation);
	}
	
	public void IncreaseDescentSpeed(float amount) {
		waveSpeed += amount;
	}
}
