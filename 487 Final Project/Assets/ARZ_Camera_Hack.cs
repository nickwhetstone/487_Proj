using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARZ_Camera_Hack : MonoBehaviour {

	public string cameraView = "";
	public GameObject stereoCameraLeft = null;
	public GameObject stereoCameraRight = null;
	public bool camerasReady = false;
	public bool needsComponents = true;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (stereoCameraLeft == null || stereoCameraRight == null) {
			GetCameraComponents ();
		} else {
			camerasReady = true;
			if (TransitionManager.InVR && needsComponents) {
				// AddCameraComponents (stereoCameraLeft);
				// AddCameraComponents (stereoCameraRight);
				needsComponents = false;
			} // TODO: remove comp when in aR
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
	private void AddCameraComponents(GameObject cam) {
		Rigidbody camRb = cam.AddComponent<Rigidbody> ();
		camRb.useGravity = true;
		camRb.isKinematic = false;
		camRb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;

		BoxCollider col = cam.AddComponent<BoxCollider> ();
		col.size = new Vector3(5f,1f,1f);
	}
}
