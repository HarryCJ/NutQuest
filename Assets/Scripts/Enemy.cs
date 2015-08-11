using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void FixedUpdate () {

	}

	public virtual void die(){
		Debug.Log("ENEMY DIES!!!");
	}

}
