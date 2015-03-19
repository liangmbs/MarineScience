using UnityEngine;
using System.Collections;
using UnityEngine .UI ;

public class HeaterInterface : MonoBehaviour {
	
	public Slider Heater;
	public Slider BaseLine;
	public float amplitude;
	public float temperature;
	public float temp;
	public float time;
	public float i = 0.0f;
	public float frequency = 1000.0f;
	public float baseline;
	
	
	// Use this for initialization
	void Start () {
	}
	
	public float getTemperature(float cycleTime)
    {
        return amplitude * Mathf.Sin(cycleTime) + baseline;
	}
	
	
	// Update is called once per frame
	void Update () {
		amplitude = Heater.value;
		baseline = BaseLine.value;
		/*time = Time.deltaTime;	
		temperature = Gettingtemperature (amplitude, time, baseline);
		i = i + 0.3f;
		print (temperature);*/
		
	}
}
