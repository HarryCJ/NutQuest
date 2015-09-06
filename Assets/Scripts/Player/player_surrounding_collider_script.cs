using UnityEngine;
using System.Collections;

public class player_surrounding_collider_script : MonoBehaviour {

	PlayerBehaviourScript parent;

	// Use this for initialization
	void Start () {
		parent = transform.parent.gameObject.GetComponent<PlayerBehaviourScript>();
	}

	// Update is called once per frame
	void FixedUpdate () {

	}

    void OnTriggerEnter2D(Collider2D other)
    {
		if (other.tag.Contains("pickup")){
			// parent.createSparkle(other.gameObject);
			Pickup myPickup = other.GetComponent<Pickup>();
			parent.addPointsAndSparkle(myPickup.getNutPoints(), other.gameObject);
			// parent.addNutPoints(myPickup.getNutPoints());
			Destroy(other.gameObject);
		}
    }
    void OnTriggerStay2D(Collider2D other)
    {
		if (other.tag.Contains("pickup")){
			// parent.addPointsAndSparkle(myPickup.getNutPoints(), other.gameObject);
			Destroy(other.gameObject);
		}
    }
    void OnTriggerExit2D(Collider2D other)
    {
        // parent.die();
    }
}
