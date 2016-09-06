using UnityEngine;
using System.Collections;
using System;

public class NutBehaviourScript : Pickup {

    Rigidbody2D myrigidbody;
    public bool isGrowing = true;
    public float limit = 20f;

	public virtual void nutStart(){
		// Debug.Log("ENEMY DIES!!!");
	}

    // Use this for initialization
    void Start()
    {
        myrigidbody = GetComponent<Rigidbody2D>();
        if (isGrowing == true){
            transform.localScale = new Vector2(0f, 0f);
            StartCoroutine(grow());
    		nutStart();
        } else {
            myrigidbody.isKinematic = false;
        }
        limit = 14f;//UnityEngine.Random.Range(10f, 20f);
    }

	// Update is called once per frame
	void FixedUpdate () {
        if (isGrowing){
            if (transform.localScale.x < limit){
                transform.position = new Vector3(transform.position.x, transform.position.y-0.005f, transform.position.z);
                transform.localScale = new Vector2(transform.localScale.x+0.1f,transform.localScale.x+0.1f);
            } else if (transform.localScale.x > limit) {
                transform.localScale = new Vector2(limit, limit);
            }
        }
	}

    IEnumerator grow(){

        float delay = UnityEngine.Random.Range(8f, 20f);
        yield return new WaitForSeconds(delay);
        isGrowing = false;
        myrigidbody.isKinematic = false;
        transform.localScale = new Vector2(limit, limit);
    }

    public override float getNutPoints(){
        return 1f;
    }

}
