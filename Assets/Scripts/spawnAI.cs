using UnityEngine;
using System.Collections;

public class spawnAI : MonoBehaviour {
	GameObject[] AIs;
	GameObject AI;
	Material[] colors;
	public float timer = 0;
	bool timing;
    int index;
    Vector3 spawn;
    int count;
	// Use this for initialization
	void Start () {
		AI = (GameObject)Resources.Load ("Patron");
		AIs = new GameObject[35];
        timing = true;
        index = 0;
        count = 1;
        spawn = GameObject.Find("AI_spawn_point").GetComponent<Transform>().position;
		GameObject t = (GameObject)Instantiate(AI, spawn, Quaternion.identity);

		colors = new Material[4];
		colors[0] = (Material)Resources.Load ("Red");
		colors[1] = (Material)Resources.Load ("Green");
		colors[2] = (Material)Resources.Load ("Blue");
		colors[3] = (Material)Resources.Load ("Yellow");

		int r = Random.Range (0, 3);

		t.GetComponentInChildren<SkinnedMeshRenderer> ().material = colors [r];
		t.GetComponent<NavAgent> ().setColor (r);

    }
	
	// Update is called once per frame
	void Update () {

	


		if (timing) {
			timer += Time.deltaTime;
		}

		if(timer > 15){

			



			//if(count < 3){
               // for (int i = 0; i < count; i++)
                //{
                    GameObject temp = (GameObject)Instantiate(AI, spawn, Quaternion.identity);
					int r = Random.Range (0, 3);
					temp.GetComponentInChildren<SkinnedMeshRenderer> ().material = colors [r];
					temp.GetComponent<NavAgent> ().setColor (r);

                    //AIs[index] = temp;
                    //index++;
                //}
                count++;
				
			//}
            timer = 0;
		}

	}
}
