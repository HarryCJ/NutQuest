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
		        if(!targets.Contains(go) && other.tag.Contains("enemy")){
		            targets.Add(go);
					Enemy myEnemy = go.GetComponent<Enemy>();
					if (myEnemy.isBouncy == true){
						parent.tryBoost();
					}
		        } else if (other.tag.Contains("env")){
					parent.resetBoostCombo();
				}

		        // foreach (GameObject child in targets){
		        //     if (child != null && child.transform.tag.Contains("enemy")){
				// 		Enemy myEnemy = child.GetComponent<Enemy>();
				// 		if (myEnemy.isDead == false){
				// 			parent.isJumping = true;
				// 			myEnemy.die();
				// 			if (myEnemy.isDead == true){
				//                 parent.jumpingTimer = 16;
				// 				parent.isBoosting = true;
				// 			}
				// 		}
				// 	}
				// }
			}
	    }
	}

	// When an enemy exits the trigger, remove it from the list
	void OnTriggerExit2D(Collider2D other){
		if (other.tag.Contains("phys") || other.tag.Contains("env")){
			parent.isGrounded = false;
			GameObject go = other.gameObject;
			if(targets.Contains(go)){
				targets.Remove(go);
			}
	    }
	}

}
