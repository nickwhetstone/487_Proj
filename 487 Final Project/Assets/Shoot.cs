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
			GameObject bullet = Instantiate (Bullet, new Vector3(transform.forward.x, transform.forward.y + .2f, transform.forward.z), transform.rotation);
			bullet.GetComponent<Rigidbody>().AddRelativeForce(new Vector3( 10, 0, 2000));
			bullet.tag = "Bullet";
		}
	}
}
