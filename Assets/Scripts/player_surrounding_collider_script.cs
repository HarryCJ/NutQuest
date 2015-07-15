using UnityEngine;
using System.Collections;

public class player_surrounding_collider_script : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

    void OnTriggerEnter2D(Collider2D other)
    {
		if (other.tag == "pickup"){
			Destroy(other.gameObject);
		}
    }
    void OnTriggerStay2D(Collider2D other)
    {
		if (other.tag == "pickup"){
			Destroy(other.gameObject);
		}
    }
    void OnTriggerExit2D(Collider2D other)
    {
        // parent.die();
    }
}
