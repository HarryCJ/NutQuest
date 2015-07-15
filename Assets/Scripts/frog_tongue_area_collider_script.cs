using UnityEngine;
using System.Collections;

public class frog_tongue_area_collider_script : MonoBehaviour {

	FrogBehaviourScript parent;

	// Use this for initialization
	void Start () {
		parent = transform.parent.gameObject.GetComponent<FrogBehaviourScript>();

	}

	// Update is called once per frame
	void Update () {

	}

    void OnTriggerEnter2D(Collider2D other)
    {
		if (other.tag == "pickup"){
	        parent.canLick = true;
		}
    }
    void OnTriggerStay2D(Collider2D other)
    {
		if (other.tag == "pickup"){
	        parent.canLick = true;
		}
    }
    void OnTriggerExit2D(Collider2D other)
    {
		// if (other.tag == "pickup"){
		// 	Debug.Log("cannot lick");
	    //     parent.canLick = false;
		// }
    }
}
