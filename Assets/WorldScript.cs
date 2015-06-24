using UnityEngine;
using System.Collections;

public class WorldScript : MonoBehaviour {
    public PolygonCollider2D pc;
	// Use this for initialization
	void Start () {

		GameObject myfrog = Instantiate(Resources.Load("frog")) as GameObject; 
		myfrog.transform.position = new Vector3(0, 5, 1);
		GameObject myfrog2 = Instantiate(Resources.Load("frog")) as GameObject; 
		myfrog.transform.position = new Vector3(0, 10, 1);
//        GameObject newFrog = new GameObject("froggy");
        
//        newFrog.transform.localScale = new Vector3(10, 10, 1);
//        SpriteRenderer frogSR = newFrog.AddComponent<SpriteRenderer>();
//        frogSR.sortingOrder = 100;
//
//        pc = newFrog.AddComponent<PolygonCollider2D>();
//        pc.SetPath(0, new Vector2[] { new Vector2(0f, 0.03f), new Vector2(-0.05f, -0.01f), new Vector2(-0.06f, -0.04f), new Vector2(0.04f, -0.04f), new Vector2(0.06f, 0.04f)});
//
//        //BoxCollider2D bc = newFrog.AddComponent<BoxCollider2D>();
//        //bc.size = new Vector3(0.12f, 0.08f, 1f);
//
//        Rigidbody2D rb = newFrog.AddComponent<Rigidbody2D>();
//        rb.fixedAngle = true;
//        rb.mass = 10;
//        FrogBehaviourScript frogBS = newFrog.AddComponent<FrogBehaviourScript>();
	}
	
	// Update is called once per frame
	void Update () {

	}
}
