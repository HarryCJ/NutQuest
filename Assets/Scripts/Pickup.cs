using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour {

	// public int nutPoints = 0;
	public int level = 1;
	public string colliderType = "box";
	public bool isProtected = false;
	public string type = "nut";

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void FixedUpdate () {

	}

	public virtual int getNutPoints(){
		return 0;
	}

	public float getCenterY(){
		if (colliderType == "box"){
			return GetComponent<BoxCollider2D>().offset.y;
		} else {
			return transform.position.y;
		}
	}

}
