using UnityEngine;
using System.Collections;

public class fish : MonoBehaviour {

    public ThermalCurve curve;
    public float size = 50;
    public float optimalGrowthPerSecond = 20;
    private float currentRate = 0;
    private bool active = false;

    public float swimmyness = 100;
    public float swimmySpeed = 2f;
    public float swimmyRandom = .01f;

    private Vector3 startingPos;
    private float swimX = 0;
    private float swimY = 0;
    private float randomX = 0;
    private float randomY = 0;
    private float rotation = 0;
    public float spinSpeed = 2f; //rotations per second

	// Use this for initialization
	void Start () {
        startingPos = transform.position;
        swimX = Random.value * Mathf.PI * 2;
        swimY = Random.value * Mathf.PI * 2;
	}

    public void activate()
    {
        active = true;
    }

    public void deactivate()
    {
        active = false;
    }

    public void growFish(float temperature, float delta)
    {
        currentRate = curve.getCurve(273 + temperature);
        size += optimalGrowthPerSecond * delta * currentRate;
    }

	// Update is called once per frame
    // This just animates the fish. Growth is handled separately so that it can be deterministic.
	void Update () {
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

        swimX += Mathf.Abs(currentRate * Time.deltaTime * swimmySpeed + randomX * swimmyRandom);
        swimY += Mathf.Abs(currentRate * Time.deltaTime * swimmySpeed + randomY * swimmyRandom);

        if (swimX > Mathf.PI * 2)
            swimX -= Mathf.PI * 2;
        if (swimY > Mathf.PI * 2)
            swimY -= Mathf.PI * 2;

        Vector3 swimPos = new Vector3(Mathf.Sin(swimX) * swimmyness, Mathf.Sin(swimY) * swimmyness, 0);

        transform.position = startingPos + swimPos;

        if (Mathf.Cos(swimX) < 0)
            rotation -= 180 * spinSpeed * Time.deltaTime;
        else
            rotation += 180 * spinSpeed * Time.deltaTime;
        if (rotation < 0)
            rotation = 0;
        if (rotation > 180)
            rotation = 180;
        transform.rotation = Quaternion.Euler(new Vector3(0, rotation, 0));
	}
}
