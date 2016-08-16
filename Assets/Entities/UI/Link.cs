using UnityEngine;
using System.Collections;

public class Link : MonoBehaviour {

    public string LinkAddress;

    void Start() {
        if (string.IsNullOrEmpty(LinkAddress)) {
            Debug.LogError("Link has no specified address.");
        }
    }

	public void OpenLink() {
        Application.OpenURL(LinkAddress);
    }
}
