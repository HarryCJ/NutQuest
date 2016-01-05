using UnityEngine;
using System.Collections;

public class spider_bite_collider_script : MonoBehaviour {

	SpiderBehaviourScript parent;

	// Use this for initialization
	void Start () {

		parent = transform.parent.gameObject.GetComponent<SpiderBehaviourScript>();

	}

	// Update is called once per frame
	void FixedUpdate () {

	}

	bool playerValid(Collider2D other){
		return (other.name == "player");
	}

    void OnTriggerEnter2D(Collider2D other)
    {
		if (playerValid(other)){
			parent.bitePlayer();
		}
    }
    void OnTriggerStay2D(Collider2D other)
    {
		if (playerValid(other)){
			parent.bitePlayer();
		}
    }
    void OnTriggerExit2D(Collider2D other)
    {

    }
}
