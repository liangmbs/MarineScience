using UnityEngine;
using System.Collections;

public class TinyOceanHeat : MonoBehaviour {

    //heat properties
    public float maxTemp = 40;
    public float minTemp = 0;
    public float surfacePos = -30;
    public float depth = 60;

    //creature
    public GameObject creature;

    //visual feedback
    //ocean
    public GameObject[] oceanLayers;
    public Color dayTime = Color.white;
    public Color nightTime = Color.blue;
    public float dayCycleLength = 3;
    public float nightTemperatureMin = .5f;
    private float daycyclePos = 1;
    private float currentTempFactor = 1;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        
	}

    //fixed update is called at a fixed interval
    void FixedUpdate()
    {
        daycyclePos += Time.fixedDeltaTime;
        if (daycyclePos > 2 * dayCycleLength)
        {
            daycyclePos -= 2 * dayCycleLength;
        }
        Renderer rend = GetComponent<Renderer>();
        float lerpTime = 0;
        if(daycyclePos <= dayCycleLength) {
            lerpTime = daycyclePos / dayCycleLength;
        } else {
            lerpTime = (dayCycleLength - (daycyclePos - dayCycleLength))/dayCycleLength;
        }
        currentTempFactor = nightTemperatureMin + (1 - lerpTime) * (1 - nightTemperatureMin);
        rend.material.color = Color.Lerp(dayTime, nightTime, lerpTime);
    }

    public float getTemperature(float x, float y)
    {
        float baseTemp = ((y - surfacePos) / depth) * maxTemp;

        return baseTemp * currentTempFactor;
    }
}
