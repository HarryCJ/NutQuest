using UnityEngine;
using System.Collections;

public class bird_surrounding_collider_script : MonoBehaviour {

    BirdBehaviourScript parent;
    GameObject player;
    PlayerBehaviourScript playerBS;

    // Use this for initialization
    void Start () {
		parent = transform.parent.gameObject.GetComponent<BirdBehaviourScript>();
		player = GameObject.Find("player");
		playerBS = player.GetComponent<PlayerBehaviourScript>();
	}

	// Update is called once per frame
	void FixedUpdate () {

        // if (parent.isCarrying == true && playerBS.isJumping == true){
        //     parent.die();
        // }
	}

    bool carryingValid(Collider2D other){
        return (other.gameObject.transform.name == "player" || other.gameObject.transform.tag.Contains("pickup") || other.gameObject.transform.tag.Contains("phys"));
    }

    void OnTriggerEnter2D(Collider2D other)
    {
		if (carryingValid(other)){
            if (parent.isCarrying == false){
                parent.isCarrying = true;
                parent.maxFlightHeight += 3;
            }
            if (other.gameObject.transform.tag.Contains("pickup")){
                other.GetComponent<Rigidbody2D>().isKinematic = false;
            }


		}
		// if (other.gameObject.transform.name == "player" && playerBS.isJumping == false){
        //     // playerBS.boost();
        //     // die();
		// } else if (other.gameObject.tag == "pickup"){
        //     Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        //     if (rb.velocity.magnitude > 3f){
        //         die();
        //     }
        // }
    }
    void OnTriggerStay2D(Collider2D other)
    {
		if (carryingValid(other)){
            if (parent.isCarrying == false){
                parent.isCarrying = true;
                parent.maxFlightHeight += 3;
            }
		}
		// if (other.gameObject.transform.name == "player" && playerBS.isJumping == false){
        //     // playerBS.boost();
        //     // die();
		// } else if (other.gameObject.tag == "pickup"){
        //     Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        //     if (rb.velocity.magnitude > 3f){
        //         die();
        //     }
        // }
    }
    void OnTriggerExit2D(Collider2D other)
    {
		if (carryingValid(other)){
            if (parent.isCarrying == true){
                parent.isCarrying = false;
                parent.maxFlightHeight -= 3;
            }
		}
        // parent.die();
    }

    // void die(){
	//
    //     parent.die();
    //     Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), parent.GetComponent<Collider2D>());
    //     Destroy(this);
    // }
}
