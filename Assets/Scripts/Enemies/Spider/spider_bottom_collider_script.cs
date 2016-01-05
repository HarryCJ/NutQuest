using UnityEngine;
using System.Collections;

public class spider_bottom_collider_script : MonoBehaviour {

	SpiderBehaviourScript parent;
	// Use this for initialization
	void Start () {
		parent = transform.parent.gameObject.GetComponent<SpiderBehaviourScript>();

	}

	// Update is called once per frame
	void FixedUpdate () {

	}

	bool groundedValid(Collider2D other){
		return (parent.isHanging == false && (other.tag.Contains("phys") || other.tag.Contains("env") || other.name == "player"));
	}

    void OnTriggerEnter2D(Collider2D other)
    {
		if (groundedValid(other)){
			parent.isGrounded = true;
		}
		// if (other.tag.Contains("phys") || other.tag.Contains("env") || other.name == "player"){
	    //     parent.isGrounded = true;
		// }
    }
    void OnTriggerStay2D(Collider2D other)
    {
		if (groundedValid(other)){
			parent.isGrounded = true;
		}
		// if (other.tag.Contains("phys") || other.tag.Contains("env") || other.name == "player"){
	    //     parent.isGrounded = true;
		// }
    }
    void OnTriggerExit2D(Collider2D other)
    {
		if (!groundedValid(other)){
			parent.isGrounded = false;
		}
    }
}
