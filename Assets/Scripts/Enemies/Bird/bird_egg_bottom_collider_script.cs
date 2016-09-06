using UnityEngine;
using System.Collections;

public class bird_egg_bottom_collider_script : MonoBehaviour {

	BirdEggBehaviourScript parent;

	// Use this for initialization
	void Start () {
		parent = transform.parent.gameObject.GetComponent<BirdEggBehaviourScript>();

	}

	// Update is called once per frame
	void FixedUpdate () {

	}

	bool groundValid(Collider2D other){
		return (other.tag.Contains("phys") || other.tag.Contains("env") || other.name == "player");
	}

    void OnTriggerEnter2D(Collider2D other)
    {
		if (groundValid(other)){
	        parent.isGrounded = true;
		}
    }
    void OnTriggerStay2D(Collider2D other)
    {
		if (groundValid(other)){
	        parent.isGrounded = true;
		}
    }
    void OnTriggerExit2D(Collider2D other)
    {
		if (groundValid(other)){
	        parent.isGrounded = false;
		}
    }
}
