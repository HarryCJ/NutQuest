using UnityEngine;
using System.Collections;

public class SparkleBehaviourScript : MonoBehaviour {

	int dieTimer = 0;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void FixedUpdate () {
		dieTimer++;

		if (dieTimer > 30){
			Destroy(gameObject);
		}
	}
}
