using UnityEngine;
using System.Collections;

public class spider_grab_collider_script : MonoBehaviour {

	SpiderBehaviourScript parent;
	// Use this for initialization
	void Start () {
		parent = transform.parent.gameObject.GetComponent<SpiderBehaviourScript>();

	}

	// Update is called once per frame
	void FixedUpdate () {

	}

	bool grabValid(Collider2D other){
		return (parent.isGrounded == false && parent.isHanging == true && parent.isGrabbing == false && (other.tag.Contains("phys") || other.name == "player") && !other.tag.Contains("env"));
	}

	// bool groundedValid(Collider2D other){
	// 	return (parent.isHanging == false && (other.tag.Contains("phys") || other.tag.Contains("env") || other.name == "player"));
	// }

    void OnTriggerEnter2D(Collider2D other)
    {
		if (grabValid(other)){
			parent.setGrabObject(other);
		}
    }
    void OnTriggerStay2D(Collider2D other)
    {
		if (grabValid(other)){
			parent.setGrabObject(other);
		}
    }
    void OnTriggerExit2D(Collider2D other)
    {
    }
}
