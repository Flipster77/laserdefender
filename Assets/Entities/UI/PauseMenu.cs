using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {

    private GameObject[] objectsToPause;
    private GameObject[] pauseMenuObjects;

    private bool gamePaused = false;

    // Use this for initialization
	void Start () {
        pauseMenuObjects = GameObject.FindGameObjectsWithTag("ShowOnPause");
        HidePauseMenu();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            objectsToPause = GameObject.FindGameObjectsWithTag("AffectedByPause");

            if (gamePaused) {
                ResumeGame();
            } else {
                PauseGame();
            }
        }
    }

    public void PauseGame() {
        Debug.Log("Game paused");

        foreach (GameObject obj in objectsToPause) {
            obj.SendMessage("OnPauseGame", SendMessageOptions.DontRequireReceiver);
        }

        ShowPauseMenu();
        gamePaused = true;
    }

    public void ResumeGame() {
        Debug.Log("Game resumed");

        foreach (GameObject obj in objectsToPause) {
            obj.SendMessage("OnResumeGame", SendMessageOptions.DontRequireReceiver);
        }

        HidePauseMenu();
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
