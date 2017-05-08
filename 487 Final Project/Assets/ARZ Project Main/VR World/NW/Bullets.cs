using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour {

	void OnCollisionEnter(Collision other) {
		Destroy (gameObject);
	}
}
