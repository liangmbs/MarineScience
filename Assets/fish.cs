using UnityEngine;
using System.Collections;

public class fish : MonoBehaviour {

    public ThermalCurve curve;
    public float size = 50;
    public float optimalGrowthPerSecond = 20;
    public Temperature temperature;

    public float swimmyness = 100;
    public float swimmySpeed = 2f;
    public float swimmyRandom = .01f;

    private Vector3 startingPos;
    private float swimX = 0;
    private float swimY = 0;
    private float randomX = 0;
    private float randomY = 0;

	// Use this for initialization
	void Start () {
        startingPos = transform.position;
        swimX = Random.value * Mathf.PI * 2;
        swimY = Random.value * Mathf.PI * 2;
	}
	
	// Update is called once per frame
	void Update () {
        float rate = curve.getCurve(273 + temperature.currentTemp) * swimmySpeed;

        size += optimalGrowthPerSecond * Time.deltaTime * rate;

        transform.localScale = new Vector3(size, size, 0);

        //swim
        randomX += (Random.value - .5f) / 10;
        randomY += (Random.value - .5f) / 10;
        if (randomX > 1)
            randomX = 1;
        if (randomX < -1)
            randomX = -1;
        if (randomY > 1)
            randomY = 1;
        if (randomY < -1)
            randomY = -1;

        swimX += rate * Time.deltaTime + randomX * swimmyRandom;
        swimY += rate * Time.deltaTime + randomY * swimmyRandom;
        if (swimX > Mathf.PI * 2)
            swimX -= Mathf.PI * 2;
        if (swimY > Mathf.PI * 2)
            swimY -= Mathf.PI * 2;

        transform.position = startingPos + new Vector3(Mathf.Sin(swimX) * swimmyness, Mathf.Sin(swimY) * swimmyness, 0);
	}
}
