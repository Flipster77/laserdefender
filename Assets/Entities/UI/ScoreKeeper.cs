using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScoreKeeper : MonoBehaviour {

	private static int score = 0;
	
	private Text scoreDisplay;
	private string prefix;

	// Use this for initialization
	void Start () {
		scoreDisplay = gameObject.GetComponent<Text>();
		prefix = scoreDisplay.text;
		
		UpdateText();
	}
	
	public static int GetScore() {
		return score;
	}
	
	public void AddPoints(int points) {
		score += points;
		UpdateText();
	}
	
	public static void Reset() {
		score = 0;
	}
	
	private void UpdateText() {
		scoreDisplay.text = prefix + " " + score;
	}
}
