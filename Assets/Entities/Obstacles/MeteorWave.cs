using UnityEngine;
using System.Collections;

public class MeteorWave : Wave {

	private MeteorSpawner meteorSpawner;
	
	public void StartWave() {
		meteorSpawner = GameObject.FindObjectOfType<MeteorSpawner>();
		meteorSpawner.StartMeteorStorm();
	}
	
	public bool WaveIsComplete() {
		return meteorSpawner.MeteorStormFinished();
	}
	
	public void IncreaseDescentSpeed(float amount) {
		// Do nothing for now
	}
}
