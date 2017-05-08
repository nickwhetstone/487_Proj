using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ARZPlayerHUD : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnGUI() {
		GUI.skin.label.fontSize = 30;
		GUI.Label(new Rect(10, 10, 200, 40), Vuforia.DefaultTrackableEventHandler.gunImageIsFound ? "Gun enabled" : "Gun disabled");
		GUI.Label(new Rect(10, 50, 200, 40), "Kill Count: " +  ARZ_Player_Control.killCount);


	}
}
