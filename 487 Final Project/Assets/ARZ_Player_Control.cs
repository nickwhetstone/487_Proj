using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARZ_Player_Control : MonoBehaviour {
	public bool camerasHookedUp = false;
	public float last = 0;

	public float speed = 10.0F;
	public float rotationSpeed = 100.0F;

	public float smooth = 0.4f;
	public float inputValue;
	public float newRotationX;
	public float newRotationY;
	public float newRotationZ;
	public float sensitivity = 6;
	private Vector3 currentAcceleration, initialAcceleration;
	public float baseSpeed = 3f;
	public GameObject gun;

	// Use this for initialization
	void Start () {
		
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
				gun.GetComponent<MeshRenderer> ().enabled = true;
				// start firing
			} else {
				gun.GetComponent<MeshRenderer> ().enabled = false;
				// stop firing
			}

			// test ();
			// UpdateMe();
			Debug.Log("-------------");
			Vector3 currentRotation = transform.localRotation.eulerAngles;

			//  -Input.gyro.rotationRateUnbiased.x
			transform.Rotate (0, -Input.gyro.rotationRateUnbiased.y,0);
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
	public void MovePlayer (Transform playerTransform, float translation, float rotation) {
		playerTransform.Translate(0, 0, -translation);
		playerTransform.Rotate(0, rotation, 0);
	}
	void test (Vector3 currentRotation, float val)
	{
		val = Mathf.Clamp (val, -0.4f, 0.4f);

		// check if the transform is at its max rotations
		// accelerometer y changes the x
		float next = currentRotation.z - val;

		// now that we have what we could possibly go to, check if it's within bounds.

		if (next > 320 && next < 360) {
			// good
			// down
			last = val;
		} else if (next > 40 && next < 320) {
			// no good
			val = -last;
		} else if (next < 40) {
			// good
			// up
			last = val;
		}
		transform.Translate (0, 0, -val);

	}
	// See more at: http://www.theappguruz.com/blog/learn-to-use-accelerometer-in-unity-in-10-mins#sthash.irrTbjuG.dpuf
}
