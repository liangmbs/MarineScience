using UnityEngine;
using System.Collections;

public class TinyCreature : MonoBehaviour {

    public float accel = 2;
    public float speed = 10;
    public float rotationSpeed = 180;
    public Rect bounds = new Rect(-100, -60, 100, 60);
    public string horizAxis = "Horizontal";
    public string vertAxis = "Vertical";

    //motion
    public Vector2 velocity = new Vector2(0, 0);
    public float friction = .5f;

    //performance
    public ThermalCurve thermalPerformance;
    public TinyOceanHeat ocean;

    //growth
    public float maxSize = 10f;
    public float growthRate = .1f; //growth per second in ideal performance.
    private float size = 1;
    public TextMesh sizeText;

	// Use this for initialization
	void Start () {
	
	}

    //FixedUpdate is called with a fixed timestep, useful for physics calculations, etc.
    void FixedUpdate()
    {
        float temperature = 273 + ocean.getTemperature(transform.position.x, transform.position.y);
        float performance = thermalPerformance.getCurve(temperature);
        Animator anim = GetComponent<Animator>();
        anim.speed = 5 * performance;

        size += growthRate * performance * Time.fixedDeltaTime;
        sizeText.text = "size: " + Mathf.FloorToInt(size * 10);
        transform.localScale = new Vector3(size, size, 0);
    }
	
	// Update is called once per frame
	void Update () {
        //get input
        float xIn = Input.GetAxis(horizAxis);
        float yIn = Input.GetAxis(vertAxis);
        //calculate velocity
        //add input
        velocity = velocity + new Vector2(xIn * accel * Time.deltaTime, yIn * accel * Time.deltaTime);
        //subtract friction
        velocity = velocity - new Vector2(velocity.x * friction * Time.deltaTime, velocity.y * friction * Time.deltaTime);
        //cap speed
        velocity = new Vector2(Mathf.Sign(velocity.x) * Mathf.Min(speed, Mathf.Abs(velocity.x)), 
            Mathf.Sign(velocity.y) * Mathf.Min(speed, Mathf.Abs(velocity.y)));
        //minimum speed
        if (Mathf.Abs(velocity.x) < 0.001)
        {
            velocity = new Vector2(0, velocity.y);
        }
        if (Mathf.Abs(velocity.y) < 0.001)
        {
            velocity = new Vector2(velocity.x, 0);
        }
        //update position
        transform.position = new Vector3(
            transform.position.x + velocity.x, 
            transform.position.y + velocity.y, 
            transform.position.z);

        if (transform.position.x < bounds.xMin)
        {
            transform.position = new Vector3(bounds.xMin, transform.position.y, transform.position.z);
        }
        if (transform.position.x > bounds.xMax)
        {
            transform.position = new Vector3(bounds.xMax, transform.position.y, transform.position.z);
        }
        if (transform.position.y < bounds.yMin)
        {
            transform.position = new Vector3(transform.position.x, bounds.yMin, transform.position.z);
        }
        if (transform.position.y > bounds.yMax)
        {
            transform.position = new Vector3(transform.position.x, bounds.yMax, transform.position.z);
        }
        //update rotation
        float targetRotation = (Mathf.Rad2Deg * Mathf.Atan2(yIn, xIn) + 90);

        transform.eulerAngles = new Vector3(0, 0, targetRotation);
	}
}
