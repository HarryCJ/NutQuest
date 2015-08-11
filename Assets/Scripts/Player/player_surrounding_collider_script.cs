using UnityEngine;
using System.Collections;

public class player_surrounding_collider_script : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void FixedUpdate () {

	}

    void OnTriggerEnter2D(Collider2D other)
    {
		if (other.tag.Contains("pickup")){
			GameObject mysparkle = Instantiate(Resources.Load("sparkle")) as GameObject;
			mysparkle.transform.position = other.transform.position;
			Destroy(other.gameObject);
		}
    }
    void OnTriggerStay2D(Collider2D other)
    {
		if (other.tag.Contains("pickup")){
			GameObject mysparkle = Instantiate(Resources.Load("sparkle")) as GameObject;
			mysparkle.transform.position = other.transform.position;
			Destroy(other.gameObject);
		}
    }
    void OnTriggerExit2D(Collider2D other)
    {
        // parent.die();
    }
}
