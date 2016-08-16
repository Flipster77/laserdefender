using UnityEngine;
using System.Collections;

public interface Wave {

	void StartWave();
		
	bool WaveIsComplete();
	
	void IncreaseDescentSpeed(float amount);
}
