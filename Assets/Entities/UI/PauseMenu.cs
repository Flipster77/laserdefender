using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {

    private GameObject[] pauseMenuObjects;

    private bool gamePaused = false;

	void Awake() {
        pauseMenuObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
        HidePauseMenu();
        Time.timeScale = 1f;
	}
	
	void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {

            if (gamePaused) {
                ResumeGame();
            } else {
                PauseGame();
            }
        }
    }

    public void PauseGame() {
        Debug.Log("Game paused");

        Time.timeScale = 0f;
        ShowPauseMenu();
        gamePaused = true;
    }

    public void ResumeGame() {
        Debug.Log("Game resumed");

        HidePauseMenu();
        Time.timeScale = 1f;
        gamePaused = false;
    }

    private void ShowPauseMenu() {

        foreach (GameObject obj in pauseMenuObjects) {
            obj.SetActive(true);
        }
    }

    private void HidePauseMenu() {

        foreach (GameObject obj in pauseMenuObjects) {
            obj.SetActive(false);
        }
    }
}
