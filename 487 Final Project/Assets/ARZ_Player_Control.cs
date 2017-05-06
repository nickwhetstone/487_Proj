using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARZ_Player_Control : MonoBehaviour {

	// Movement Settings
	public float speed = 10.0F;
	public float rotationSpeed = 100.0F;
	public GameObject rigidBodyFPSControllerPrefab;
	public GameObject fpsController;
	// ARZ MAIN
	private GameObject arzMain;
	private ARZ_Camera_Hack arzCameraHack;
	public bool fpsInstantiated = false;
	public bool camerasHookedUp = false;
	// Use this for initialization
	void Start () {
		arzMain = GameObject.FindGameObjectWithTag ("ARZ_MAIN");
		arzCameraHack = arzMain.GetComponent<ARZ_Camera_Hack> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!fpsInstantiated && TransitionManager.InVR) {
			Transform parentTransform = GameObject.FindGameObjectWithTag ("Player").transform;
			fpsController = Instantiate (rigidBodyFPSControllerPrefab, parentTransform);
			fpsController.transform.localPosition = new Vector3 (0f,fpsController.transform.localScale.y / 2,0f);//.zero;
			fpsController.GetComponent<Rigidbody> ().collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;

			GameObject arCamera = GameObject.FindGameObjectWithTag ("ARCamera");
			arCamera.transform.SetParent(fpsController.transform,false);


			fpsInstantiated = true;
		}
		if (!camerasHookedUp && fpsInstantiated) {
			/*if (arzCameraHack.camerasReady) {
				// Move Cameras
				GameObject camLeft = arzCameraHack.stereoCameraLeft;
				GameObject camRight = arzCameraHack.stereoCameraRight;
				camLeft.transform.parent = fpsController.transform;
				camRight.transform.parent = fpsController.transform;

				camLeft.transform.localPosition = new Vector3(0.1f,0.6f,0f);
				camRight.transform.localPosition = new Vector3(0.1f,0.6f,0f);

				camRight.transform.localRotation = new Quaternion(0f,0f,0f,0f);
				camRight.transform.localRotation = new Quaternion(0f,0f,0f,0f);
				camerasHookedUp = true;
			}*/
		}
			
		if (TransitionManager.InVR) {

			float translation = Input.GetAxis ("Vertical") * speed;
			float rotation = Input.GetAxis ("Horizontal") * rotationSpeed;

			translation *= Time.deltaTime;
			rotation *= Time.deltaTime;


		}
	}
}
