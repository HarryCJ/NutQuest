using UnityEngine;
using System.Collections;

public class frog_tongue_end_script : MonoBehaviour {

	frog_tongue_script parent;

	// Use this for initialization
	void Start () {

		parent = transform.parent.gameObject.GetComponent<frog_tongue_script>();

	}

	// Update is called once per frame
	void FixedUpdate () {

	}

	bool pickupValid(Collider2D other){
		return (other.tag.Contains("pickup") && parent.withdrawing == false && parent.thrustTimer > 0);
	}

	bool playerValid(Collider2D other){
		return (other.name == "player" && parent.withdrawing == false && parent.thrustTimer > 0);
	}

    void OnTriggerEnter2D(Collider2D other)
    {
		if (pickupValid(other)){
			parent.withdraw(other);
		} else if (playerValid(other)){
			parent.hitPlayer();
		}
    }
    void OnTriggerStay2D(Collider2D other)
    {
		if (pickupValid(other)){
			parent.withdraw(other);
		} else if (playerValid(other)){
			parent.hitPlayer();
		}
    }
    void OnTriggerExit2D(Collider2D other)
    {

    }
}
