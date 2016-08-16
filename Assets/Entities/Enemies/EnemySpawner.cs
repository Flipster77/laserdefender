using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour {

	public GameObject weakVFormation;
	public GameObject weakTrackingFormation;
	public GameObject mediumTrackingFormation;
	public GameObject mediumLeaderFormation;
	
	private WaveCounter waveCounter;
	
	private Queue<Wave> waveQueue;
	private Wave currentWave;
	
	public float increaseSpeedAmount = 0.1f;

	void Awake() {
		ResetStats();
		SetupWaves();
	}

	// Use this for initialization
	void Start() {
		waveCounter = GameObject.FindObjectOfType<WaveCounter>();
		StartNextWave();
	}
	
	// Update is called once per frame
	void Update() {
		
		if (currentWave.WaveIsComplete()) {
			//Debug.Log("Wave " + WaveCounter.GetWaveNumber() + " is complete");
			StartNextWave();
		}
	}
	
	private void SetupWaves() {
		waveQueue = new Queue<Wave>();
		
		EnemyWave tempWave = new EnemyWave();
		tempWave.AddEnemyFormation(weakVFormation);
		waveQueue.Enqueue(tempWave);
		
		tempWave = new EnemyWave();
		tempWave.AddEnemyFormation(weakTrackingFormation);
		waveQueue.Enqueue(tempWave);
		
		tempWave = new EnemyWave();
		tempWave.AddEnemyFormation(mediumLeaderFormation);
		waveQueue.Enqueue(tempWave);
		
		tempWave = new EnemyWave();
		tempWave.AddEnemyFormation(mediumTrackingFormation);
		waveQueue.Enqueue(tempWave);
		
		MeteorWave meteorWave = new MeteorWave();
		waveQueue.Enqueue(meteorWave);
	}
	
	private void ResetStats() {
		Debug.Log ("Resetting counters");
		ScoreKeeper.Reset();
		WaveCounter.Reset();
	}
	
	private void StartNextWave() {
		//Debug.Log ("Next wave starting " + gameObject.GetInstanceID());
		
		if (WaveCounter.GetWaveNumber() % waveQueue.Count == 0) {
			foreach (Wave wave in waveQueue) {
				wave.IncreaseDescentSpeed(increaseSpeedAmount);
			}
		}
		
		DisplayWaveNumber();
		currentWave = waveQueue.Dequeue();
		currentWave.StartWave();
		waveQueue.Enqueue(currentWave);
	}
	
	private void DisplayWaveNumber() {
		waveCounter.ReachedNextWave();
	}
}
