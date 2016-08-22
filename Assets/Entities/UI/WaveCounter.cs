using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WaveCounter : MonoBehaviour {

	private static int waveNumber = 0;
	
	private Text waveDisplay;
	private string prefix;

	// Use this for initialization
	void Awake () {
		waveDisplay = gameObject.GetComponent<Text>();
		prefix = waveDisplay.text;
		
		UpdateText();
	}
	
	public static int GetWaveNumber() {
		return waveNumber;
	}
	
	public void ReachedNextWave() {
		waveNumber++;
		UpdateText();
	}
	
	public static void Reset() {
		waveNumber = 0;
	}
	
	private void UpdateText() {
		waveDisplay.text = prefix + " " + waveNumber;
	}
}
