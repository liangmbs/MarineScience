using UnityEngine;
using System.Collections;

public class TinyOceanHeat : MonoBehaviour {

    public float maxTemp = 40;
    public float minTemp = 0;
    public float depth = 50;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //fixed update is called at a fixed interval
    void FixedUpdate()
    {

    }

    public float getTemperature(float x, float y)
    {
        return (y / depth) * (maxTemp - minTemp) + minTemp;
    }
}
