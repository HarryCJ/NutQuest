using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class player_bottom_collider_script : MonoBehaviour {

	PlayerBehaviourScript parent;
	// Collider2D myother;

	// Use this for initialization
	void Start () {
		parent = transform.parent.gameObject.GetComponent<PlayerBehaviourScript>();

	}

	// Update is called once per frame
	void FixedUpdate () {

	}

    // void OnTriggerEnter2D(Collider2D other)
    // {
	// 	if (other.tag.Contains("phys") || other.tag.Contains("env")){
	// 	    parent.isGrounded = true;
	// 		myother = other;
	// 	}
    // }
    void OnTriggerStay2D(Collider2D other)
    {
		if (other.tag.Contains("phys") || other.tag.Contains("env")){
	    	parent.isGrounded = true;
		}
    }
    // void OnTriggerExit2D(Collider2D other)
    // {
	// 	if (other.tag.Contains("phys") || other.tag.Contains("env")){
    //     	parent.isGrounded = false;
	// 	}
    // }

	// Set up a list to keep track of targets
	public System.Collections.Generic.List<GameObject> targets = new System.Collections.Generic.List<GameObject>();

	// If a new enemy enters the trigger, add it to the list of targets
	void OnTriggerEnter2D(Collider2D other){
		if (other.tag.Contains("phys") || other.tag.Contains("env")){
			if (parent.isGrounded == false){
				parent.isBoosting = false;
		    	parent.isGrounded = true;
		        GameObject go = other.gameObject;
		        if(!targets.Contains(go)){
		            targets.Add(go);
		        }
			}
	    }
	}

	// When an enemy exits the trigger, remove it from the list
	void OnTriggerExit2D(Collider2D other){
		if (other.tag.Contains("phys") || other.tag.Contains("env")){
	    	parent.isGrounded = false;
	      GameObject go = other.gameObject;
	      targets.Remove(go);
	    }
	}

}
