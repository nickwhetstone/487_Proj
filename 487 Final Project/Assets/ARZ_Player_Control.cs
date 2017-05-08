using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARZ_Player_Control : MonoBehaviour {
	public bool camerasHookedUp = false;
	public float speed = 10.0F;
	public float rotationSpeed = 100.0F;

	public static int killCount = 0;

	public float smooth = 1f;
	public float newRotation;
	public float sensitivity = 30;

	private Vector3 currentAcceleration, initialAcceleration;
	public float baseSpeed = 3f;
	public GameObject gun;

	void Start()
	{
		initialAcceleration = Input.acceleration;
		currentAcceleration = Vector3.zero;
	}

		
	// Update is called once per frame
	void Update () {
		if (!camerasHookedUp && TransitionManager.InVR) {
			GameObject userHead = GameObject.FindGameObjectWithTag ("UserHead");
			userHead.transform.parent = this.transform;
			userHead.transform.localPosition = Vector3.zero;
			userHead.transform.localRotation = Quaternion.identity;
			Screen.orientation = ScreenOrientation.Landscape; // TODO: might not be needed
			camerasHookedUp = true;
			Input.gyro.enabled = true;
		}
		if (camerasHookedUp) {

			if (Vuforia.DefaultTrackableEventHandler.gunImageIsFound) {
				gun.SetActive(true);
				// start firing
			} else {
				gun.SetActive(false);
				// stop firing
			}

			// test ();
			UpdateMe();
			Debug.Log("-------------");
			Vector3 currentRotation = transform.localRotation.eulerAngles;

			//  -Input.gyro.rotationRateUnbiased.x
			// transform.Rotate (0, -Input..y * sensitivity,0);
			// transform.Rotate (0, 0, );
			float translation = 0;


			if (Vuforia.DefaultTrackableEventHandler.forwardImageIsFound) {
				translation = baseSpeed;
			}
			Debug.Log (translation);

			translation *= Time.deltaTime;
			transform.Translate(0, 0, translation);

			// transform.Translate (0, 0, -Input.gyro.rotationRateUnbiased.x);
			// test(currentRotation,Input.gyro.rotationRateUnbiased.x);
		}
	}
	void UpdateMe () {
		//pre-processing
		currentAcceleration = Vector3.Lerp(currentAcceleration, Input.acceleration - initialAcceleration, Time.deltaTime/smooth);
		newRotation = Mathf.Clamp(currentAcceleration.x * sensitivity, -1, 1);
		transform.Rotate(0, currentAcceleration.x * sensitivity, 0);
	}
	// See more at: http://www.theappguruz.com/blog/learn-to-use-accelerometer-in-unity-in-10-mins#sthash.irrTbjuG.dpuf
}
