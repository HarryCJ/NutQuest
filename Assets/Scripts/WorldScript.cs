using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldScript : MonoBehaviour {
    // public PolygonCollider2D pc;
    // Use this for initialization

    List<GameObject> gameObjects = new List<GameObject>();

    void Start () {

        // addFrog(new Vector3(0, 10, 0), true);
        // addFrog(new Vector3(10, 10, 0), false);
        // addFrog(new Vector3(-10, 10, 0), true);

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

        StartCoroutine(spawnFrogs());
    }

    IEnumerator spawnFrogs()
    {
        while (true)
        {
            float delay = UnityEngine.Random.Range(0.5f, 10);
            yield return new WaitForSeconds(delay);

            if (UnityEngine.Random.Range(0, 2) == 0){

                addFrog(new Vector3(25, -3, 0), false);

            } else {

                addFrog(new Vector3(-25, -3, 0), true);

            }

        }
    }

    void addFrog(Vector3 pos, bool directionIsRight){
      	GameObject myfrog = Instantiate(Resources.Load("frog")) as GameObject;
      	myfrog.transform.position = pos;
        FrogBehaviourScript frogBS = myfrog.GetComponent<FrogBehaviourScript>();
        frogBS.directionIsRight = directionIsRight;
    }

    // Update is called once per frame
    void Update () {

    }
}
