using UnityEngine;
using System.Collections;

public class frog_tongue_end_script : MonoBehaviour {

	frog_tongue_script parent;

	// Use this for initialization
	void Start () {

		parent = transform.parent.gameObject.GetComponent<frog_tongue_script>();

	}

	// Update is called once per frame
	void FixedUpdate () {

	}

    void OnTriggerEnter2D(Collider2D other)
    {
		if (other.tag.Contains("pickup") && parent.withdrawing == false && parent.thrustTimer > 0){
			parent.withdraw(other);
		}
    }
    void OnTriggerStay2D(Collider2D other)
    {
		if (other.tag.Contains("pickup") && parent.withdrawing == false && parent.thrustTimer > 0){
			parent.withdraw(other);
		}
    }
    void OnTriggerExit2D(Collider2D other)
    {

    }
}
