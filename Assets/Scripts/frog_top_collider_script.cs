using UnityEngine;
using System.Collections;

public class frog_top_collider_script : MonoBehaviour {

    FrogBehaviourScript parent;
    GameObject player;
    PlayerBehaviourScript playerBS;

    // Use this for initialization
    void Start () {
		parent = transform.parent.gameObject.GetComponent<FrogBehaviourScript>();
		player = GameObject.Find("player");
		playerBS = player.GetComponent<PlayerBehaviourScript>();
	}

	// Update is called once per frame
	void Update () {

	}

    void OnTriggerEnter2D(Collider2D other)
    {
		if (other.gameObject.transform.name == "player" && playerBS.isJumping == false){
	        parent.die();
            playerBS.boost();
            Physics2D.IgnoreCollision(other.GetComponent<Collider2D>(), parent.GetComponent<Collider2D>());
            Destroy(this);
		}
    }
    void OnTriggerStay2D(Collider2D other)
    {
		if (other.gameObject.transform.name == "player" && playerBS.isJumping == false){
            playerBS.boost();
            die();
		} else if (other.gameObject.tag == "pickup"){
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
            if (rb.velocity.magnitude > 3f){
                die();
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        // parent.die();
    }

    void die(){

        parent.die();
        Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), parent.GetComponent<Collider2D>());
        Destroy(this);
    }
}
