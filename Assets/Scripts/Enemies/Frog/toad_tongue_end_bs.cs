using UnityEngine;
using System.Collections;

public class toad_tongue_end_bs : MonoBehaviour {

	ToadBehaviourScript parent;

	// Use this for initialization
	void Start () {

		parent = transform.parent.gameObject.GetComponent<ToadBehaviourScript>();
	
	}

	bool pickupValid(Collider2D other){
		return (other.tag.Contains("pickup") && parent.pickup == null);
	}

	bool playerValid(Collider2D other){
		return (other.name == "player");
	}

    void OnTriggerEnter2D(Collider2D other)
    {
		if (pickupValid(other)){
			parent.setLickItem(other.transform);
		} else if (playerValid(other)){
			parent.hitPlayer();
		}
    }
    void OnTriggerStay2D(Collider2D other)
    {
		if (pickupValid(other)){
			parent.setLickItem(other.transform);
		} else if (playerValid(other)){
			parent.hitPlayer();
		}
    }
    void OnTriggerExit2D(Collider2D other)
    {

    }
}
