using UnityEngine;
using System.Collections;

public class frog_bottom_collider_script : MonoBehaviour {

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
        parent.isGrounded = true;
    }
    void OnTriggerStay2D(Collider2D other)
    {
        parent.isGrounded = true;
    }
    void OnTriggerExit2D(Collider2D other)
    {
        parent.isGrounded = false;
    }
}
