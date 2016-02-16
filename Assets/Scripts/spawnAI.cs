using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class spawnAI : MonoBehaviour {
    public int identifier;
	GameObject AI;
    

    Vector3 spawn;



    spawnGlobal sg;
    
    Scare[] scareObjects;
    // Use this for initialization
    void Start () {
		AI = (GameObject)Resources.Load ("newPatron");
       


        
        spawn = GameObject.Find("AI_spawn_point"+identifier+"").GetComponent<Transform>().position;
        sg = GameObject.Find("MetaSpawn").GetComponent<spawnGlobal>();


    }
	
	// Update is called once per frame
	void Update () {


	}

    public void genAI()
    {
        GameObject temp = (GameObject)Instantiate(AI, spawn, Quaternion.identity);
        temp.GetComponent<NavAgent>().setSpawnTag(identifier);

    }





    //reset ability to scare on each wave
    public void resetScares()
    {
        //foreach(Scare s in scareObjects)
        //{
        //    s.resetUsed();
        //}
    }
}
