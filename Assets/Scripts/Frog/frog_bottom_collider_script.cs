using UnityEngine;
using System.Collections;

public class frog_bottom_collider_script : MonoBehaviour {

	FrogBehaviourScript parent;

	// Use this for initialization
	void Start () {
		parent = transform.parent.gameObject.GetComponent<FrogBehaviourScript>();

	}

	// Update is called once per frame
	void FixedUpdate () {

	}

    void OnTriggerEnter2D(Collider2D other)
    {
		if (other.tag.Contains("phys") || other.tag.Contains("env") || other.name == "player"){
	        parent.isGrounded = true;
		}
    }
    void OnTriggerStay2D(Collider2D other)
    {
		if (other.tag.Contains("phys") || other.tag.Contains("env") || other.name == "player"){
	        parent.isGrounded = true;
		}
    }
    void OnTriggerExit2D(Collider2D other)
    {
		if (other.tag.Contains("phys") || other.tag.Contains("env") || other.name == "player"){
	        parent.isGrounded = false;
		}
    }
}
