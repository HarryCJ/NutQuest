using UnityEngine;
using System.Collections;

public class NutSpawnerBehaviourScript : MonoBehaviour {

	// Use this for initialization
	void Start () {

        //StartCoroutine(spawnNuts());
	}
	
	// Update is called once per frame
    void Update()
    {
	}
    
	IEnumerator spawnNuts(){

		Debug.Log ("spawnNuts");
		//Random rnd = new Random();
		int i = 0;

        while (true)
        {
            float delay = UnityEngine.Random.Range(0.5f, 20f);
            yield return new WaitForSeconds(delay);

            //GameObject newNut = new GameObject("nutty" + i);
            //newNut.transform.position = transform.position;
            //newNut.transform.localScale = new Vector3(10, 10, 1);

            //SpriteRenderer nutSR = newNut.AddComponent<SpriteRenderer>();
            //nutSR.sortingOrder = 20000 - (int)transform.position.y;

            //BoxCollider2D bc = newNut.AddComponent<BoxCollider2D>();
            //bc.size = new Vector3(0.05f, 0.05f, 1f);
            //bc.offset = new Vector3(0, -0.01f, 0);
            //Rigidbody2D rb = newNut.AddComponent<Rigidbody2D>();
            //rb.fixedAngle = true;
            //rb.mass = 5;
            //NutBehaviourScript nutPhys = newNut.AddComponent<NutBehaviourScript>();


            i++;
        }
    }
}
