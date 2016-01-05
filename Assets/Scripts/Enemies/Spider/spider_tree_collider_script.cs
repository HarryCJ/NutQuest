using UnityEngine;
using System.Collections;

public class spider_tree_collider_script : MonoBehaviour {

	SpiderBehaviourScript parent;
	// Use this for initialization
	void Start () {
		parent = transform.parent.gameObject.GetComponent<SpiderBehaviourScript>();

	}

	// Update is called once per frame
	void FixedUpdate () {

	}

	bool climbingValid(Collider2D other){
		return (other.tag.Contains("climbable"));
	}

	// bool groundedValid(Collider2D other){
	// 	return (parent.isHanging == false && (other.tag.Contains("phys") || other.tag.Contains("env") || other.name == "player"));
	// }

    void OnTriggerEnter2D(Collider2D other)
    {
		if (climbingValid(other)){
			parent.setClimbing(true);
		}
    }
    void OnTriggerStay2D(Collider2D other)
    {
    }
    void OnTriggerExit2D(Collider2D other)
    {
		if (climbingValid(other)){
			parent.setClimbing(false);
		}
    }
}
