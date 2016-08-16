using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerSelector : MonoBehaviour {

	public GameObject[] playerShips;
	
	public bool selectionScreen;
	
	public Image shipImage;
	
	// Text elements
	public Text shipName;
	public Text shipSpeed;
	public Text shipLasers;
	public Text shipNumOfCannons;
	public Text shipHealth;
	
	public Button previousShip;
	public Button nextShip;
	

	private static int currentShipIndex = 0;
	private PlayerShip currentShip;
	
	void Awake() {
		if (selectionScreen) {
			UpdatePlayerShip();
		} else { // Otherwise it is the game, create the ship
			CreatePlayerShip();
		}
	}
	
	public GameObject GetCurrentShip() {
		return playerShips[currentShipIndex];
	}
	
	public void NextShip() {
		if (currentShipIndex+1 < playerShips.Length) {
			ChooseShip(currentShipIndex+1);
		}
	}
	
	public void PreviousShip() {
		
	}
	
	public void ChooseShip(int index) {
		if (index < playerShips.Length) {
			currentShipIndex = index;
			UpdatePlayerShip();
		} else {
			Debug.LogError("Attempted to set player ship index to invalid index: " + index);
		}
	}
	
	public static void Reset() {
		currentShipIndex = 0;
	}
	
	private void CreatePlayerShip() {
		Vector3 playerStartingPosition = new Vector3(0f, -4f, 0f);
	
		Instantiate(playerShips[currentShipIndex], playerStartingPosition, Quaternion.identity);
	}
	
	private void UpdatePlayerShip() {
		GameObject currentShipChoice = playerShips[currentShipIndex];
		currentShip = currentShipChoice.GetComponent<PlayerShip>();
		
		UpdateShipImage();
		UpdateShipName();
		UpdateShipSpeed();
		UpdateShipLasers();
		UpdateShipHealth();
		UpdateActiveButtons();
	}
	
	private void UpdateShipImage() {
		SpriteRenderer shipRenderer = playerShips[currentShipIndex].GetComponent<SpriteRenderer>();
		shipImage.sprite = shipRenderer.sprite;
	}
	
	private void UpdateShipName() {
		shipName.text = currentShip.shipName;
	}
	
	private void UpdateShipSpeed() {
		string speed = "<SPEED>";
		
		if (currentShip.speed > 6) {
			speed = "Fast";
		} else if (currentShip.speed > 4) {
			speed = "Medium";
		} else {
			speed = "Slow";
		}
	
		shipSpeed.text = "Speed: " + speed;
	}
	
	private void UpdateShipLasers() {
		string lasers = "<SPEED>";
		
		Laser basicLaser = currentShip.basicLaser.GetComponent<Laser>();
		
		if (basicLaser.Damage >= 2) {
			lasers = "Strong";
		} else {
			lasers = "Weak";
		}
		
		shipLasers.text = "Lasers: " + lasers;
		shipNumOfCannons.text = "Num of cannons: " + currentShip.numShotsPerFire;
	}
	
	private void UpdateShipHealth() {
		shipHealth.text = "Health: " + currentShip.maxHealth;
	}
	
	private void UpdateActiveButtons() {
		if (currentShipIndex <= 0) {
			previousShip.interactable = false;
		} else {
			previousShip.interactable = true;
		}
		
		if (currentShipIndex < playerShips.Length-1) {
			nextShip.interactable = true;
		} else {
			nextShip.interactable = false;
		}
	}
}
