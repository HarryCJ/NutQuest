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

    void OnTriggerStay2D(Collider2D other)
    {
    }

	// Set up a list to keep track of targets
	public System.Collections.Generic.List<GameObject> targets = new System.Collections.Generic.List<GameObject>();

	// If a new enemy enters the trigger, add it to the list of targets
	void OnTriggerEnter2D(Collider2D other){

		if (other.tag.Contains("phys") || other.tag.Contains("env")){

			GameObject go = other.gameObject;
			if(!targets.Contains(go)){

				targets.Add(go);
				parent.setGrounded(true);
				// parent.isGrounded = true;
				parent.isBoosting = false;

		        if(other.tag.Contains("enemy")){

					Enemy myEnemy = go.GetComponent<Enemy>();
					if (myEnemy.isBouncy == true){

						parent.tryBoost(myEnemy.jumpLevels[parent.environment_ws.upgrades[myEnemy.enemyType+"_boost"]], myEnemy.addToMultiplier);
					}
		        } else if (other.tag.Contains("env")){

					parent.resetBoostCombo();
				}
			}
	    }

	}

	// When an enemy exits the trigger, remove it from the list
	void OnTriggerExit2D(Collider2D other){
		if (other.tag.Contains("phys") || other.tag.Contains("env")){
			GameObject go = other.gameObject;
			if(targets.Contains(go)){
				targets.Remove(go);

				targets.RemoveAll(item => item == null);
				if (targets.Count == 0){
					parent.setGrounded(false);
					// parent.isGrounded = false;
				}
			}
	    }
	}

}
