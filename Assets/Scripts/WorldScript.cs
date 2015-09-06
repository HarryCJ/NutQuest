using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class WorldScript : MonoBehaviour {
    // public PolygonCollider2D pc;
    // Use this for initialization

    List<GameObject> gameObjects = new List<GameObject>();
    int timer = 99;

    List<GameObject> waveDisplayUIText  = new List<GameObject>();
    List<Text> waveDisplayUITextText  = new List<Text>();

    void Start () {

		waveDisplayUIText.Add(GameObject.Find("waveDisplayUIText1"));
		waveDisplayUITextText.Add(waveDisplayUIText[0].GetComponent<Text>());
		waveDisplayUIText.Add(GameObject.Find("waveDisplayUIText1.5"));
		waveDisplayUITextText.Add(waveDisplayUIText[1].GetComponent<Text>());
		waveDisplayUIText.Add(GameObject.Find("waveDisplayUIText2"));
		waveDisplayUITextText.Add(waveDisplayUIText[2].GetComponent<Text>());
		waveDisplayUIText.Add(GameObject.Find("waveDisplayUIText2.5"));
		waveDisplayUITextText.Add(waveDisplayUIText[3].GetComponent<Text>());
		waveDisplayUIText.Add(GameObject.Find("waveDisplayUIText3"));
		waveDisplayUITextText.Add(waveDisplayUIText[4].GetComponent<Text>());

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

        StartCoroutine(spawnBirds());
        StartCoroutine(spawnFrogs());
        StartCoroutine(spawnClouds());

        StartCoroutine(CountdownWave());
    }

    IEnumerator spawnFrogs()
    {
        while (true)
        {
            float delay = UnityEngine.Random.Range(5f, 10f);
            // delay = UnityEngine.Random.Range(0.5f, 0.5f);
            yield return new WaitForSeconds(delay);

            if (UnityEngine.Random.Range(0, 2) == 0){

                addFrog(new Vector3(25, -3, 0), false);

            } else {

                addFrog(new Vector3(-25, -3, 0), true);

            }

        }
    }

    IEnumerator spawnBirds()
    {
        while (true)
        {
            float delay = UnityEngine.Random.Range(6f, 10f);
            // delay = UnityEngine.Random.Range(0.5f, 0.5f);
            yield return new WaitForSeconds(delay);

            if (UnityEngine.Random.Range(0, 2) == 0){

                addBird(new Vector3(25, 4, 0), false);

            } else {

                addBird(new Vector3(-25, 4, 0), true);

            }
        }
    }

    IEnumerator spawnClouds()
    {
        while (true)
        {
            float delay = UnityEngine.Random.Range(10f, 15f);
            yield return new WaitForSeconds(delay);

            GameObject mycloud = Instantiate(Resources.Load("Prefabs/cloud")) as GameObject;
            CloudBehaviourScript cloudBS = mycloud.GetComponent<CloudBehaviourScript>();

    		//pick sprite
    		SpriteRenderer sr = mycloud.GetComponent<SpriteRenderer>();
    		Sprite[] sprites = Resources.LoadAll<Sprite>(@"sprites");
    		string[] cloudNames = new string[]{"cloud_1", "cloud_2"};
    		int cloudSprite = UnityEngine.Random.Range(0, cloudNames.Length);
    		for (int x = 0; x < sprites.Length; x++) {
    			if (sprites[x].name == cloudNames[cloudSprite]){
    				sr.sprite = sprites[x];
    			}
    		}

    		//pick opacity
    		Color myC = sr.color;
    		myC.a = UnityEngine.Random.Range(0.2f, 0.9f);
    		sr.color = myC;

    		//pick speed
    		cloudBS.speed = UnityEngine.Random.Range(0.001f, 0.005f);

    		//pick location
    		cloudBS.mypos = new Vector3(-25f, UnityEngine.Random.Range(0.2f, 10f), 0.5f);

            //pick size
        }
    }

    void addFrog(Vector3 pos, bool directionIsRight){
      	GameObject myfrog = Instantiate(Resources.Load("Prefabs/frog")) as GameObject;
      	myfrog.transform.position = pos;
        FrogBehaviourScript frogBS = myfrog.GetComponent<FrogBehaviourScript>();
        frogBS.directionIsRight = directionIsRight;
    }

    void addBird(Vector3 pos, bool directionIsRight){
      	GameObject mybird = Instantiate(Resources.Load("Prefabs/bird")) as GameObject;
      	mybird.transform.position = pos;
        BirdBehaviourScript birdBS = mybird.GetComponent<BirdBehaviourScript>();
        birdBS.directionIsRight = directionIsRight;
    }

    // Update is called once per frame
    void FixedUpdate () {
    }

	IEnumerator CountdownWave(){

        while (timer > 0)
        {
            yield return new WaitForSeconds(1f);
            timer--;
            StartCoroutine(WaveDisplayTransition());
        }
    }

	IEnumerator WaveDisplayTransition(){

		foreach (Text t in waveDisplayUITextText){
			t.text = "";
		}

		string waveTimerString = timer.ToString();
		if (waveTimerString.Length == 1){
			waveDisplayUITextText[2].text = waveTimerString;
		} else if (waveTimerString.Length == 2){
			waveDisplayUITextText[1].text = waveTimerString.Substring(0, 1);
			waveDisplayUITextText[3].text = waveTimerString.Substring(1, 1);
		} else if (waveTimerString.Length == 3){
			waveDisplayUITextText[0].text = waveTimerString.Substring(0, 1);
			waveDisplayUITextText[2].text = waveTimerString.Substring(1, 1);
			waveDisplayUITextText[4].text = waveTimerString.Substring(2, 1);
		}

		// waveDisplayUITextText[2].fontSize = 100;
		foreach (Text t in waveDisplayUITextText){
			t.fontSize = 80;
		}
        yield return new WaitForSeconds(0.005f);
		foreach (Text t in waveDisplayUITextText){
			t.fontSize = 84;
		}
        yield return new WaitForSeconds(0.005f);
		foreach (Text t in waveDisplayUITextText){
			t.fontSize = 88;
		}
        yield return new WaitForSeconds(0.005f);
		foreach (Text t in waveDisplayUITextText){
			t.fontSize = 92;
		}
        yield return new WaitForSeconds(0.005f);
		foreach (Text t in waveDisplayUITextText){
			t.fontSize = 96;
		}
        yield return new WaitForSeconds(0.005f);
		foreach (Text t in waveDisplayUITextText){
			t.fontSize = 100;
		}
        yield return new WaitForSeconds(0.005f);
		foreach (Text t in waveDisplayUITextText){
			t.fontSize = 96;
		}
        yield return new WaitForSeconds(0.005f);
		foreach (Text t in waveDisplayUITextText){
			t.fontSize = 92;
		}
        yield return new WaitForSeconds(0.005f);
		foreach (Text t in waveDisplayUITextText){
			t.fontSize = 88;
		}
        yield return new WaitForSeconds(0.005f);
		foreach (Text t in waveDisplayUITextText){
			t.fontSize = 84;
		}
        yield return new WaitForSeconds(0.005f);
		foreach (Text t in waveDisplayUITextText){
			t.fontSize = 80;
		}
	}
}
