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
			if (myPickup.isProtected == false){
				if (myPickup.type == "nut"){
					parent.addPointsAndSparkle(myPickup.getNutPoints(), other.gameObject, false);
				} else {
					parent.addPointsAndSparkle(myPickup.getNutPoints(), other.gameObject, true);
				}
				// parent.addNutPoints(myPickup.getNutPoints());
				Destroy(other.gameObject);
			}
		} else if (other.tag.Contains("snowflake")){

			SnowBehaviourScript sbs = other.GetComponent<SnowBehaviourScript>();
			if (sbs.isRed){
				parent.getHit(1, parent.directionIsRight);
			}


		}
    }
    void OnTriggerStay2D(Collider2D other)
    {
		if (other.tag.Contains("pickup")){
			// parent.addPointsAndSparkle(myPickup.getNutPoints(), other.gameObject);
			// Destroy(other.gameObject);
		}
    }
    void OnTriggerExit2D(Collider2D other)
    {
        // parent.die();
    }
}
