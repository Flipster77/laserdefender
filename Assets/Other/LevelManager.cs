using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public void LoadLevel(string name) {
		Debug.Log("Level load requested for: " + name);
        SceneManager.LoadScene(name);
    }
	
	public void LoadNextLevel() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

        Debug.Log("Loading level " + (currentSceneIndex + 1).ToString());
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
	
	public void QuitRequest() {
		Debug.Log("Quit requested");
		Application.Quit();
	}
	
	public void NewGame() {
		LoadLevel("Ship Selection");
	}
	
	public void GameOver() {
		StartCoroutine(WaitThenLoadLevel("End Screen"));
	}
	
	private IEnumerator WaitThenLoadLevel(string levelName) {
		yield return new WaitForSeconds(3);
		LoadLevel(levelName);
	}

}
