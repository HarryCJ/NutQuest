using UnityEngine;
using System.Collections;

public class frog_tongue_end_script : MonoBehaviour {

	frog_tongue_script parent;

	// Use this for initialization
	void Start () {

		parent = transform.parent.gameObject.GetComponent<frog_tongue_script>();

	}

	// Update is called once per frame
	void Update () {

	}

    void OnTriggerEnter2D(Collider2D other)
    {
		Debug.Log("enter");
		Debug.Log(other.name);
		Debug.Log(parent.thrustTimer);
		Debug.Log(parent.withdrawing);
		if (other.tag == "pickup" && parent.withdrawing == false && parent.thrustTimer > 0){
			Debug.Log("call withdraw");
			parent.withdraw(other);
		}
    }
    void OnTriggerStay2D(Collider2D other)
    {
		Debug.Log("enter");
		Debug.Log(other.name);
		Debug.Log(parent.thrustTimer);
		Debug.Log(parent.withdrawing);
		if (other.tag == "pickup" && parent.withdrawing == false && parent.thrustTimer > 0){
			Debug.Log("call withdraw");
			parent.withdraw(other);
		}
    }
    void OnTriggerExit2D(Collider2D other)
    {

    }
}
