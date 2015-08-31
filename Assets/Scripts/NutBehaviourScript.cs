using UnityEngine;
using System.Collections;
using System;

public class NutBehaviourScript : Pickup {

    Rigidbody2D myrigidbody;

    // Use this for initialization
    void Start()
    {
        myrigidbody = GetComponent<Rigidbody2D>();
        StartCoroutine(grow());
    }

	// Update is called once per frame
	void FixedUpdate () {
	}

    IEnumerator grow(){

        float delay = UnityEngine.Random.Range(8f, 20f);
        yield return new WaitForSeconds(delay);
        myrigidbody.isKinematic = false;
    }

    public override int getNutPoints(){
        return 1;
    }

}
