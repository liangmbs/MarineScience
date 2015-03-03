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
		print (heater.temperature);
        currentTime += Time.deltaTime;

        float realCycle = cycleTime / (Mathf.PI * 2);

        if (currentTime / realCycle > Mathf.PI * 2)
        {
            currentTime -= Mathf.PI * 2 * realCycle;
        }

        heaterTemp = heater.getTemperature(currentTime / realCycle);
        outsideTemp = baseTemp + variability * Mathf.Sin(currentTime / realCycle);
        currentTemp = outsideTemp + heaterTemp;
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
