using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour {

	// public int nutPoints = 0;
	public int level = 1;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void FixedUpdate () {

	}

	public virtual int getNutPoints(){
		return 0;
	}

}
