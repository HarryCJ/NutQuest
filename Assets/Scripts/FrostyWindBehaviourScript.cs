using UnityEngine;
using System.Collections;
using System;

public class FrostyWindBehaviourScript : MonoBehaviour {

	float posYTarget = 0f;
	public bool directionIsRight = false;

	// Use this for initialization
	void Start () {
		posYTarget = transform.position.y + 5f;
		StartCoroutine(moveDelay());
	}
	
	// Update is called once per frame
	// void Update () {
	
	// }

	void FixedUpdate () {
	}

	IEnumerator moveDelay(){

		// transform.rotation *= Quaternion.Euler(0f, 0f, -2.5f);

        // transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, posYTarget, transform.position.z), Time.deltaTime*1f);
        if (directionIsRight == true){
        	transform.position = new Vector3(transform.position.x+0.05f, ((float) Math.Sin(transform.position.x)*2f), transform.position.z);
        } else {
        	transform.position = new Vector3(transform.position.x-0.05f, ((float) Math.Sin(transform.position.x)*2f), transform.position.z);
        }

        if (transform.position.y < posYTarget + 0.01f && transform.position.y > posYTarget - 0.01f){
        	if (transform.position.y < posYTarget){
        		posYTarget = transform.position.y - 10f;
        	} else {
        		posYTarget = transform.position.y + 10f;
        	}
        }
		yield return new WaitForSeconds(0.001f);
		StartCoroutine(moveDelay());
	}
}
