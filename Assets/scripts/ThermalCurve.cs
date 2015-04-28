using UnityEngine;
using System.Collections;

public class ThermalCurve : MonoBehaviour {

    static float MAX = 273 + 40;
    static float MIN = 273 + 0;

    public float interval = 0.1f;
    public float[] data;
    //public dataChart chart;
    //TODO: the datachart should have a slot for a thermalcurve, 
    //and also track the cursor, colors, etc.

    public float optimalTemp = 295.15f;
    public float arrhenBreadth = 4258;
    public float arrhenLower = 7457;
    public float arrhenUpper = 19664;
    public float lowerBound = 286;
    public float upperBound = 298;

    //temperature graph
    //TODO: re-create this functionality in the chart object.
    /*
    public GameObject temperatureCursor;
    public float tempXTravel = 10;
    private float tempXStart = 0;
    public Color tempGoodColor = Color.green;
    public Color tempOkColor = Color.yellow;
    public Color tempBadColor = Color.red;*/

	// Use this for initialization
	void Start () {
        //tempXStart = temperatureCursor.transform.position.x;
        //updateChart();
	}

    public float getCurve(float temp)
    {
        float performance = Mathf.Exp(arrhenBreadth / optimalTemp - arrhenBreadth / temp) *
                (1 + Mathf.Exp(arrhenLower / optimalTemp - arrhenLower / lowerBound) +
                    Mathf.Exp(arrhenUpper / upperBound - arrhenUpper / optimalTemp)) /
                (1 + Mathf.Exp(arrhenLower / temp - arrhenLower / lowerBound) +
                    Mathf.Exp(arrhenUpper / upperBound - arrhenUpper / temp));
        if(performance > 1) {
            performance = 1;
        }

        /*temperatureCursor.transform.position = new Vector3(
            tempXStart + tempXTravel * ((temp - 273) / 40 - 0.5f),
            temperatureCursor.transform.position.y,
            temperatureCursor.transform.position.z);
        Renderer tempRend = temperatureCursor.GetComponent<Renderer>();
        if(performance > .5f) {
            tempRend.sharedMaterial.color = Color.Lerp(tempOkColor, tempGoodColor, (performance - .5f) * 2);
        }
        else
        {
            tempRend.sharedMaterial.color = Color.Lerp(tempBadColor, tempOkColor, performance * 2);
        }*/

        return performance;
    }

    /*void updateChart()
    {
        int length = 2 + (int)((MAX - MIN) * (1 / interval));
        data = new float[length];
        for (int i = 0; i < length; i++)
        {
            float temp = MIN + i * interval;
            data[i] = getCurve(temp);
        }

        if (chart != null)
        {
            chart.setData(data);
        }
    }*/
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnDrawGizmos()
    {
        //updateChart();
    }
}
