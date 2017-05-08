using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

	public GameObject Bullet;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate() {
		if(Vuforia.DefaultTrackableEventHandler.gunImageIsFound) {
			Debug.Log ("Fire in the hole!");
			GameObject bullet = Instantiate (Bullet, transform.forward, transform.rotation);
			bullet.GetComponent<Rigidbody>().AddRelativeForce(new Vector3( 50, 50, 2000));
			bullet.tag = "Bullet";
		}
	}
}
