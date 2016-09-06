using UnityEngine;
using System.Collections;

public class raven_bottom_collider_script : MonoBehaviour {

	RavenBehaviourScript parent;

	// Use this for initialization
	void Start () {
		parent = transform.parent.gameObject.GetComponent<RavenBehaviourScript>();

	}

	// Update is called once per frame
	void FixedUpdate () {

	}

	// string[] flyingActions = { "goToNewNestAction", "findFoodAction" };

	bool groundValid(Collider2D other){
		// foreach (string s in flyingActions)
		// {
		// 	if (s == parent.currentAction){
		// 		return false;
		// 	}
		// }
		return !parent.isFlying && (other.tag.Contains("phys") || other.tag.Contains("env") || other.name == "player");
	}

    void OnTriggerEnter2D(Collider2D other)
    {
		if (groundValid(other)){
	        parent.setGrounded(true);
		}
    }
    void OnTriggerStay2D(Collider2D other)
    {
		if (groundValid(other)){
	        parent.setGrounded(true);
		}
    }
    void OnTriggerExit2D(Collider2D other)
    {
		if (other.tag.Contains("phys") || other.tag.Contains("env") || other.name == "player"){
	        parent.setGrounded(false);
		}
    }
}
