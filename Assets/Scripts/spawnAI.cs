using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class spawnAI : MonoBehaviour {
    public int identifier;
	GameObject AI;
    GameObject mom;
    GameObject dad;

    Vector3 spawn;



    spawnGlobal sg;
    
    Scare[] scareObjects;
    // Use this for initialization
    void Start () {
		AI = (GameObject)Resources.Load ("newPatron");
        mom = (GameObject)Resources.Load("Mom");
        dad = (GameObject)Resources.Load("Dad");




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

    public void genMom()
    {
        GameObject temp = (GameObject)Instantiate(mom, spawn, Quaternion.identity);
        temp.GetComponent<NavAgent>().setSpawnTag(identifier);

    }

    public void genDad()
    {
        GameObject temp = (GameObject)Instantiate(dad, spawn, Quaternion.identity);
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
