using UnityEngine;
using System.Collections.Generic;

public class SwimmingHolder : MonoBehaviour {
    public PlayerManager player;
    private List<SwimmingCreature> creatures;
    public List<GameObject> prefabs;
    //number of swimming creatures for each speices. make sure to update this
    public List<int> speciesNumbers; 

	// Use this for initialization
	void Start () {
	    creatures = new List<SwimmingCreature>();
        speciesNumbers = new List<int>();
        foreach(CharacterManager c in player.species) {
            speciesNumbers.Add(0);
        }
	}
	
	// Update is called once per frame
	void Update () {
        for (int i = 0; i < player.species.Count; i++)
        {
            int speciesAmount = Mathf.FloorToInt(player.species[i].speciesAmount);
            if (speciesAmount != speciesNumbers[i])
            {
                if (speciesAmount > speciesNumbers[i])
                {
                    while (speciesNumbers[i] < speciesAmount)
                    {
                        AddCreature(i);
                        speciesNumbers[i]++;
                    }
                }
                else if (speciesAmount < speciesNumbers[i])
                {
                    //figure out the cause of death
                    CharacterManager man = player.species[i];
                    ParticleSystem pSys;
                    if (man.lastEaten > man.lastStarve + man.lastCool + man.lastHot)
                    {
                        pSys = player.eatenPart;
                    } else if (man.lastStarve != 0) {
                        pSys = player.starvedPart;
                    }
                    else if (man.lastHot != 0)
                    {
                        pSys = player.tooHotPart;
                    }
                    else
                    {
                        pSys = player.tooCoolPart;
                    }

                    //remove them
                    while (speciesNumbers[i] > speciesAmount)
                    {
                        RemoveCreature(i, pSys);
                        speciesNumbers[i]--;
                    }
                }
            }
        }
	}

    void AddCreature(int cId)
    {
        //if we're running in the editor, we can instantiate as prefabs. 
        //This lets us change variables in the prefab and see the effects
#if UNITY_EDITOR
        GameObject cObj = (GameObject)UnityEditor.PrefabUtility.InstantiatePrefab(prefabs[cId]);
#else
        GameObject cObj = GameObject.Instantiate(prefabs[cId]);
#endif
        SwimmingCreature c = cObj.GetComponent<SwimmingCreature>();
        creatures.Add(c);
        c.creatureFlock = creatures;
        c.id = cId;
        c.Spawn();
    }

    void RemoveCreature(int cId, ParticleSystem cause)
    {
        bool foundOne = false;
        int index = 0;
        int found = -1;
        while (index < creatures.Count && !foundOne)
        {
            if (creatures[index].id == cId && !creatures[index].isDying)
            {
                foundOne = true;
                found = index;
            }
            else
            {
                index++;
            }
        }

        if (foundOne)
        {
            SwimmingCreature c = creatures[found];
            creatures.Remove(c);
            c.startDying(cause);
        }
    }
}
