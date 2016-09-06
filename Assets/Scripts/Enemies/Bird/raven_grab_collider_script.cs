using UnityEngine;
using System.Collections;

public class raven_grab_collider_script : MonoBehaviour {

	RavenBehaviourScript parent;

	// Use this for initialization
	void Start () {
		parent = transform.parent.parent.gameObject.GetComponent<RavenBehaviourScript>();

	}

	// Update is called once per frame
	void FixedUpdate () {

	}

	string[] flyingActions = { "goToNewNestAction", "findFoodAction" };

	bool groundValid(Collider2D other){
		return (other.tag.Contains("pickup") || other.tag.Contains("enemy"));
	}

    void OnTriggerEnter2D(Collider2D other)
    {
		if (groundValid(other)){
	        parent.setPickup(other);
		}
    }
  //   void OnTriggerStay2D(Collider2D other)
  //   {
		// if (groundValid(other)){
	 //        parent.isGrounded = true;
		// }
  //   }
  //   void OnTriggerExit2D(Collider2D other)
  //   {
		// if (groundValid(other)){
	 //        parent.isGrounded = false;
		// }
  //   }
}
