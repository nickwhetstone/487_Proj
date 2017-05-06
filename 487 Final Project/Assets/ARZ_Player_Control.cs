using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARZ_Player_Control : MonoBehaviour {
	public bool camerasHookedUp = false;

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
			camerasHookedUp = true;
		}
	}
}
