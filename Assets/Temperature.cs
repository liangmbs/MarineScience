using UnityEngine;
using System.Collections;

public class Temperature : MonoBehaviour {

    public float baseTemp = 20;
    public float variability = 20;
    public float cycleTime = 3;

    private float currentTime = 0;

    public float currentTemp;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        currentTime += Time.deltaTime;

        float realCycle = cycleTime / (Mathf.PI * 2);

        if (currentTime / realCycle > Mathf.PI * 2)
        {
            currentTime -= Mathf.PI * 2 * realCycle;
        }
        currentTemp = baseTemp + variability * Mathf.Sin(currentTime / realCycle);
	}
}
