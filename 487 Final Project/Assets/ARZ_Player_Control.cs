using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARZ_Player_Control : MonoBehaviour {

	// Movement Settings
	public float speed = 10.0F;
	public float rotationSpeed = 100.0F;

	// ARZ MAIN
	private GameObject arzMain;
	private ARZ_Camera_Hack arzCameraHack;

	// Use this for initialization
	void Start () {
		arzMain = GameObject.FindGameObjectWithTag ("ARZ_MAIN");
		arzCameraHack = arzMain.GetComponent<ARZ_Camera_Hack> ();
	}
	
	// Update is called once per frame
	void Update () {

		/********* GET INPUT *********/

		float translation = Input.GetAxis ("Vertical") * speed;
		float rotation = Input.GetAxis ("Horizontal") * rotationSpeed;

		translation *= Time.deltaTime;
		rotation *= Time.deltaTime;

		/********* END GET INPUT *********/

		/********* HANDLE INPUT *********/

		if (arzCameraHack.camerasReady) {
			// Move Cameras
			arzCameraHack.MoveCameras (translation, rotation);
			// Move Player (which this is attached to)
			// TODO
			// GIVE PLAYER A BODY
			// MAKE SURE PLAYER AND CAMERA MOVE THE SAME!
			// MEANING, if a player runs into a wall, the camera should stop too!
		}

		/********* END HANDLE INPUT *********/

	}
}
