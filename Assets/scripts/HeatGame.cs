using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HeatGame : MonoBehaviour {

    public fish fish;
    public Temperature temperature;
    public Text sizeText;
    public Text tempText;
    public int numCycles = 7;
    public float cycleLength = 5; // in seconds

    bool running = false;
    float cycleTimer = 0;
    int cycleCounter = 0;


	// Use this for initialization
	void Start () {
        if (fish == null || temperature == null)
        {
            print("uh-oh: you're missing a gameobject link in the HeatGame!");
        }
	}
	
	// Update is called once per frame
	void Update () {
       
	}

    //FixedUpdate is called at a steady interval (which is better for game stuff like physics)
    void FixedUpdate()
    {
        if (cycleCounter >= numCycles)
        {
            running = false;
            fish.deactivate();
        }

        if (running)
        {
            float t = Time.fixedDeltaTime;
            cycleTimer += t;
            if (cycleTimer >= cycleLength)
            {
                cycleTimer = 0;
                cycleCounter++;
            }
            float currentTemp = temperature.getHeat(cycleTimer / cycleLength);
            fish.growFish(currentTemp, t);
            sizeText.text = "Size: " + Mathf.Floor(fish.size);
            tempText.text = "Temperature: " + Mathf.Floor(currentTemp);
        }
    }

    public void PlayButtonPress()
    {
        running = true;
        fish.activate();
    }
}
