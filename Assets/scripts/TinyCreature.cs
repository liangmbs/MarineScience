using UnityEngine;
using System.Collections;

public class TinyCreature : MonoBehaviour {

    public float accel = 2;
    public float speed = 10;
    public float rotationSpeed = 180;
    public Rect bounds = new Rect(-100, 0, 100, 60);
    public string horizAxis = "Horizontal";
    public string vertAxis = "Vertical";

    //motion
    public Vector2 velocity = new Vector2(0, 0);
    public float friction = .5f;

	// Use this for initialization
	void Start () {
	
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
        //update rotation
        float targetRotation = (Mathf.Rad2Deg * Mathf.Atan2(yIn, xIn) + 90);

        transform.eulerAngles = new Vector3(0, 0, targetRotation);
	}
}
