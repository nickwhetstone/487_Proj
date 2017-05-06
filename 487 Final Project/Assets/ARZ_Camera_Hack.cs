using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARZ_Camera_Hack : MonoBehaviour {

	public string cameraView = "";
	public GameObject stereoCameraLeft = null;
	public GameObject stereoCameraRight = null;
	public bool camerasReady = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (stereoCameraLeft == null || stereoCameraRight == null) {
			GetCameraComponents ();
		} else {
			camerasReady = true;
		}
	}
	public void MoveCamera (Transform camTransform, float translation, float rotation) {
		camTransform.Translate(0, 0, translation);
		camTransform.Rotate(0, rotation, 0);
	}
	public void MoveCameras (float translation, float rotation) {
		MoveCamera (stereoCameraRight.transform, translation, rotation);
		MoveCamera (stereoCameraLeft.transform, translation, rotation);
	}
	private void GetCameraComponents() {
		foreach (GameObject camObject in GameObject.FindGameObjectsWithTag("MainCamera")) {
			if (camObject.name == "StereoCameraLeft") {
				stereoCameraLeft = camObject;
				Debug.Log ("Found Cam Left");
			} else if (camObject.name == "StereoCameraRight") {
				stereoCameraRight = camObject;
				Debug.Log ("Found Cam Right");
			} 
		}
	}
}
