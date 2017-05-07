using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARZ_Player_Control : MonoBehaviour {
	public bool camerasHookedUp = false;
	public float lastY = 0;
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
		}
		if (camerasHookedUp) {
			Vector3 acc = Input.acceleration;
			float x = acc.x;
			float y = acc.y;
			float z = acc.z;

			// clamp the current rotation speed
			Debug.Log ("Y before: " + y);
			y = Mathf.Clamp (y, -0.4f, 0.4f);
			Debug.Log ("Y after: " + y);


			// check if the transform is at its max rotations
			Vector3 currentRotation = transform.localRotation.eulerAngles;
			// accelerometer y changes the x
			float nextX = currentRotation.x - y;

			// now that we have what we could possibly go to, check if it's within bounds.

			Debug.Log ("currentRotation.x: " + currentRotation.x);
			bool noGood = false;
			if (nextX > 320 && nextX < 360) {
				// good
				// down
				lastY = y;
			} else if (nextX > 40 && nextX < 320) {
				// no good
				y = -lastY;
			} else if (nextX < 40) {
				// good
				// up
				lastY = y;
			}

			Debug.Log ("Next X: " + nextX);
			Debug.Log ("Final Y: " + y);
			transform.Rotate (y, -x, 0);

		}
	}
	public void MovePlayer (Transform playerTransform, float translation, float rotation) {
		playerTransform.Translate(0, 0, translation);
		playerTransform.Rotate(0, rotation, 0);
	}

}
