using UnityEngine;
using System.Collections;

public class Temperature : MonoBehaviour {

    public float baseTemp = 20;
    public float variability = 20;
    public float cycleTime = 3;

    private float currentTime = 0;

    public float outsideTemp = 0;
    public float heaterTemp = 0;
    public float currentTemp = 0;

	public HeaterInterface heater;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

    //cycle time is the position in the current cycle
    //should be between 0 and 1
    public float getHeat(float cycleTime)
    {
        float piTime = cycleTime * Mathf.PI * 2;

        heaterTemp = heater.getTemperature(piTime);
        outsideTemp = baseTemp + variability * Mathf.Sin(piTime);
        currentTemp = outsideTemp + heaterTemp;
        return currentTemp;
    }

    void OnDrawGizmos()
    {
        for (int i = 0; i < 3; i++)
        {
            float x = transform.localScale.x;
            switch (i)
            {
                case 0:
                    Gizmos.color = Color.red;
                    x *= heaterTemp;
                    break;
                case 1:
                    Gizmos.color = Color.blue;
                    x *= outsideTemp;
                    break;
                case 2:
                    Gizmos.color = Color.magenta;
                    x *= currentTemp;
                    break;
            }
            Vector3 pos = transform.position;
            Gizmos.DrawLine(pos + new Vector3(x, 0, 0), pos + new Vector3(x, transform.localScale.y, 0));
        }
    }
}
