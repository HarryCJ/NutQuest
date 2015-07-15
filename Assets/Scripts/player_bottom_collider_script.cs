using UnityEngine;
using System.Collections;

public class player_bottom_collider_script : MonoBehaviour {

	PlayerBehaviourScript parent;

	// Use this for initialization
	void Start () {
		parent = transform.parent.gameObject.GetComponent<PlayerBehaviourScript>();

	}

	// Update is called once per frame
	void Update () {

	}

    void OnTriggerEnter2D(Collider2D other)
    {
		if (other.name != "nutArea"){
		    parent.isGrounded = true;
		}
    }
    void OnTriggerStay2D(Collider2D other)
    {
		if (other.name != "nutArea"){
		    parent.isGrounded = true;
		}
    }
    void OnTriggerExit2D(Collider2D other)
    {
		if (other.name != "nutArea"){
        	parent.isGrounded = false;
		}
    }
}
