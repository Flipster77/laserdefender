using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MusicPlayer : MonoBehaviour {

	private static MusicPlayer instance = null;
	
	public AudioClip startClip;
	public AudioClip gameClip;
	public AudioClip endClip;
	
	private AudioSource music;
	
	void Awake () {
		
		if (instance != null && instance != this) {
			GameObject.Destroy(gameObject);
		} else {
			instance = this;
			GameObject.DontDestroyOnLoad(gameObject);
			SetupMusic();
		}
	}
	
	void Start() {
		music.Play();
	}
	
	void OnLevelWasLoaded(int level) {
		// Only continue for the true instance of MusicPlayer
		if (instance != this) {
			return;
		}
		
		AudioClip newClip = music.clip;
		
        /*
         * Note: Need to keep this switch statement updated when scenes are
         *       added to the build settings.
         */
		switch(level) {
			case 5:
				newClip = gameClip;
				break;
			case 6:
				newClip = endClip;
				break;
			default:
				newClip = startClip;
				break;
		}
		
		if (newClip != music.clip) {
			music.Stop();
			music.clip = newClip;
			music.loop = true;
			music.Play();
		}
	}
	
	private void SetupMusic() {
		if (music == null) {
			music = GetComponent<AudioSource>();
			music.clip = startClip;
			music.loop = true;
		}
	}
}
