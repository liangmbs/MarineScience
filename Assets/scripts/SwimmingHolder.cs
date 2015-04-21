using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

public class SwimmingHolder : MonoBehaviour {

    public List<SwimmingCreature> creatures;
    public List<GameObject> prefabs;

	// Use this for initialization
	void Start () {
	    creatures = new List<SwimmingCreature>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Jump"))
        {
            AddCreature();
        }
        if (Input.GetButton("Cancel"))
        {
            for (int i = 0; i < 30; i++)
            {
                AddCreature();
            }
        }
	}

    void AddCreature()
    {
        //GameObject cObj = GameObject.Instantiate(cyplo);
        GameObject cObj;
        if (Random.value < 0.1)
        {
            cObj = (GameObject)PrefabUtility.InstantiatePrefab(prefabs[2]);
        }
        else if (Random.value < 0.2)
        {
            cObj = (GameObject)PrefabUtility.InstantiatePrefab(prefabs[1]);
        } else 
        {
            cObj = (GameObject)PrefabUtility.InstantiatePrefab(prefabs[0]);
        }
        SwimmingCreature c = cObj.GetComponent<SwimmingCreature>();
        creatures.Add(c);
        c.creatureFlock = creatures;
        c.Spawn();
    }
}
