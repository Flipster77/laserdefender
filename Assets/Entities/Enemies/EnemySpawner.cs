using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour {

	public GameObject weakVFormation;
	public GameObject weakTrackingFormation;
	public GameObject mediumTrackingFormation;
	public GameObject mediumLeaderFormation;
	
	private WaveCounter waveCounter;

    private List<Wave> wavesToShuffle;
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
		
		if (currentWave == null || currentWave.WaveIsComplete()) {
			//Debug.Log("Wave " + WaveCounter.GetWaveNumber() + " is complete");
			StartNextWave();
		}
	}
	
	private void SetupWaves() {
        wavesToShuffle = new List<Wave>();
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
		
		/*if (WaveCounter.GetWaveNumber() % waveQueue.Count == 0) {
			foreach (Wave wave in waveQueue) {
				wave.IncreaseDescentSpeed(increaseSpeedAmount);
			}
		}*/
		
		UpdateWaveNumber();
		currentWave = waveQueue.Dequeue();
		currentWave.StartWave();

        // If the wave queue is empty
        if (waveQueue.Count == 0) {

            // Shuffle the wave order
            ShuffleWaves(wavesToShuffle);

            // Increase the descent speed for the next lot of waves
            // Add them back to the queue
            foreach (Wave wave in wavesToShuffle) {
                wave.IncreaseDescentSpeed(increaseSpeedAmount);
                waveQueue.Enqueue(wave);
            }
            wavesToShuffle.Clear();

            // Enqueue the current wave as the meteor wave is always last
            waveQueue.Enqueue(currentWave);
        }
        // Add the current wave to be shuffled for the next round
        else {
            wavesToShuffle.Add(currentWave);
        }
	}
	
	private void UpdateWaveNumber() {
		waveCounter.ReachedNextWave();
	}

    private void ShuffleWaves(IList<Wave> waveList) {
        for (int i = 0; i < waveList.Count; i++) {
            Wave temp = waveList[i];
            int randomIndex = Random.Range(i, waveList.Count);
            waveList[i] = waveList[randomIndex];
            waveList[randomIndex] = temp;
        }
    }
}
