using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {

	public GameObject Bullet;
	public AudioSource fireSound;
	public Transform bulletSpawn;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate() {
		if(Vuforia.DefaultTrackableEventHandler.gunImageIsFound) {
			Debug.Log ("Fire in the hole!");
		/*	GameObject bullet = Instantiate (Bullet, new Vector3(transform.forward.x, transform.forward.y + .2f, transform.forward.z), transform.rotation);
			bullet.GetComponent<Rigidbody>().AddRelativeForce(new Vector3( 10, 0, 2000));
			Debug.Log(transform.forward.x);
			//fireSound.time = .1f;
			*/
			Fire ();
		}
	}
	void Fire()
	{
		// Create the Bullet from the Bullet Prefab
		var bullet = (GameObject)Instantiate(
			Bullet,
			bulletSpawn.position,
			bulletSpawn.rotation);

		// Add velocity to the bullet
		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 6f;
		bullet.tag = "Bullet";
		// Destroy the bullet after 2 seconds
		// Destroy(bullet, 2.0f);        
	}
}
