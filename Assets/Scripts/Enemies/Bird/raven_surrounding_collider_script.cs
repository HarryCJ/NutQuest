using UnityEngine;
using System.Collections;

public class raven_surrounding_collider_script : MonoBehaviour {

    GameObject player;
    // PlayerBehaviourScript playerBS;

    // Use this for initialization
    void Start () {

	}

	// Update is called once per frame
	void FixedUpdate () {

	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.transform.tag.Contains("pickup")){
            other.GetComponent<Rigidbody2D>().isKinematic = false;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {

    }

    void OnTriggerExit2D(Collider2D other)
    {

    }
}
