using UnityEngine;
using System.Collections.Generic;

public class SwimmingCreature : MonoBehaviour {

    //boundary
    public Rect bounds = new Rect(-20, -20, 20, 20);

    public float edgeRadius = 5f;
    public float edgePower = 1;

    public int level = 1;

    public int id = 0;

    public List<SwimmingCreature> creatureFlock;

    //radii for responding to other creatures
    public float alignRadius = 2;
    public float attractRadius = 2;
    public float avoidRadius = .5f;
    public float fleeRadius = 10;
    public float huntRadius = 10;
    public float eatRadius = 1;

    public float alignPower = 1;
    public float attractPower = 1;
    public float avoidPower = 1;
    public float fleePower = 1;
    public float huntPower = 1;

    //save our squared distances to avoid square-root calculations when comparing distances
    private float alignSq = 4;
    private float attractSq = 4;
    private float avoidSq = .25f;
    private float fleeSq = 100;
    private float huntSq = 100;
    private float eatSq = 1;

    //maximum movement speed
    public float maxSpeed = 30;
    //maximum steering force
    public float maxForce = .03f;

    //movement
    public Vector2 velocity = new Vector2(0, 0);
    public Vector2 acceleration = new Vector2(0, 0);

    //animator
    private Animator anim;
    

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        alignSq = Mathf.Pow(alignRadius, 2);
        attractSq = Mathf.Pow(attractRadius, 2);
        avoidSq = Mathf.Pow(avoidRadius, 2);
        fleeSq = Mathf.Pow(fleeRadius, 2);
        huntSq = Mathf.Pow(huntRadius, 2);
        eatSq = Mathf.Pow(eatRadius, 2);

        if (creatureFlock != null)
        {
            Flock(creatureFlock);
        }

        velocity += acceleration;
        //cap velocity
        if (velocity.magnitude > maxSpeed)
        {
            velocity = velocity.normalized * maxSpeed;
        }

        //scale relative to framerate
        Vector3 frameVel = velocity * Time.deltaTime;

        //animate speed
        anim.speed = velocity.magnitude / maxSpeed;

        //move
        transform.position = new Vector3(transform.position.x + frameVel.x,
            transform.position.y + frameVel.y, transform.position.z);

        // get rotation relative to UP
        float zRot = Mathf.Atan2(velocity.y, velocity.x) - Mathf.Atan2(-1, 0);
        //set rotation
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, zRot * Mathf.Rad2Deg));
    }

    //updates the 
    private void Flock(List<SwimmingCreature> creatures)
    {
        acceleration = Vector2.zero;

        Vector2 avoidTotal = Vector2.zero;
        Vector2 attractTotal = Vector2.zero;
        Vector2 alignTotal = Vector2.zero;
        Vector2 fleeTotal = Vector2.zero;
        Vector2 huntTotal = Vector2.zero;
        int avoidCount = 0;
        int attractCount = 0;
        int alignCount = 0;
        int fleeCount = 0;
        int huntCount = 0;

        foreach (SwimmingCreature c in creatures)
        {
            //don't flock with ourself
            if (c.GetInstanceID() != GetInstanceID())
            {
                Vector3 ourPos = transform.position;
                Vector3 theirPos = c.transform.position;
                //calculate the squared distance
                //this is faster than the real distance because we don't need to do square-root calculations
                float distSq = Mathf.Pow((ourPos.x - theirPos.x), 2) +
                    Mathf.Pow((ourPos.y - theirPos.y), 2);

                float powerRatio = Mathf.Max(1, 1 / distSq);

                //flock within level
                if (c.level == level)
                {
                    Vector2 av = Avoid(c, distSq);
                    if (av != Vector2.zero)
                    {
                        avoidTotal += av * powerRatio;
                        avoidCount++;
                    }
                    Vector2 at = Attract(c, distSq);
                    if (at != Vector2.zero)
                    {
                        attractTotal += at * powerRatio;
                        attractCount++;
                    }
                    Vector2 al = Align(c, distSq);
                    if (al != Vector2.zero)
                    {
                        alignTotal += al * powerRatio;
                        alignCount++;
                    }
                }
                else if (c.level > level)
                {
                    Vector2 fl = Flee(c, distSq);
                    if (fl != Vector2.zero)
                    {
                        fleeTotal += fl * powerRatio;
                        fleeCount++;
                    }
                }
                else
                {
                    Vector2 hn = Hunt(c, distSq);
                    if (hn != Vector2.zero)
                    {
                        huntTotal += hn * powerRatio;
                        huntCount++;
                    }

                    eat(c, distSq);
                }
            }
        }

        if (avoidCount != 0)
            avoidTotal = avoidTotal / avoidCount;
        if (attractCount != 0)
            attractTotal = attractTotal / attractCount;
        if (alignCount != 0)
            alignTotal = alignTotal / alignCount;
        if (fleeCount != 0)
            fleeTotal = fleeTotal / fleeCount;
        if (huntCount != 0)
            huntTotal = huntTotal / huntCount;

        acceleration = 
            EdgeSteer() * edgePower+ 
            avoidTotal * avoidPower + 
            attractTotal * attractPower +
            alignTotal * alignPower + 
            fleeTotal * fleePower+ 
            huntTotal * huntPower;

        if (acceleration.magnitude > maxForce)
        {
            acceleration = acceleration.normalized * maxForce;
        }
    }

    private Vector2 Avoid(SwimmingCreature creature, float distSq)
    {
        if (distSq < avoidSq)
        {
            Vector3 ourPos = transform.position;
            Vector3 theirPos = creature.transform.position;
            //go away from the other creature
            Vector2 avoidVector = new Vector2(ourPos.x - theirPos.x, ourPos.y - theirPos.y);
            avoidVector.Normalize();
            //TODO: weight by distance
            return avoidVector;
        }
        else
            return Vector2.zero;
    }

    private Vector2 Attract(SwimmingCreature creature, float distSq)
    {
        if (distSq < attractSq)
        {
            Vector3 ourPos = transform.position;
            Vector3 theirPos = creature.transform.position;
            //go towards the other creature
            Vector2 attractVector = new Vector2(theirPos.x - ourPos.x, theirPos.y - ourPos.y);
            attractVector.Normalize();
            //TODO: weight by distance
            return attractVector;
        }
        else
            return Vector2.zero;
    }

    private Vector2 Align(SwimmingCreature creature, float distSq)
    {
        if (distSq < alignSq)
        {
            //align with the other creature
            Vector2 alignVector = creature.velocity.normalized;
            alignVector.Normalize();
            //TODO: weight by distance
            return alignVector;
        }
        else
            return Vector2.zero;
    }

    private Vector2 Flee(SwimmingCreature creature, float distSq)
    {
        if (distSq < fleeSq)
        {
            Vector3 ourPos = transform.position;
            Vector3 theirPos = creature.transform.position;
            //go away from the other creature
            Vector2 avoidVector = new Vector2(ourPos.x - theirPos.x, ourPos.y - theirPos.y);
            avoidVector.Normalize();
            //TODO: weight by distance
            return avoidVector;
        }
        else
            return Vector2.zero;
    }

    private Vector2 Hunt(SwimmingCreature creature, float distSq)
    {
        if (distSq < huntSq)
        {
            Vector3 ourPos = transform.position;
            Vector3 theirPos = creature.transform.position;
            //go towards the other creature
            Vector2 attractVector = new Vector2(theirPos.x - ourPos.x, theirPos.y - ourPos.y);
            attractVector.Normalize();
            //TODO: weight by distance
            return attractVector;
        }
        else
            return Vector2.zero;
    }

    private Vector2 EdgeSteer()
    {
        float x = transform.position.x;
        float y = transform.position.y;
        Vector2 steer = Vector2.zero;

        //"analog" steering (CPU intense)
        /*
        if (x < bounds.xMin + edgeRadius)
            steer += new Vector2((edgeRadius - (bounds.xMin - x)) / -edgeRadius, 0);
        if (x > bounds.xMax - edgeRadius)
            steer += new Vector2((edgeRadius - (bounds.xMax - x)) / -edgeRadius, 0);
        if (y < bounds.yMin + edgeRadius)
            steer += new Vector2(0, (edgeRadius - (bounds.yMax - y)) / -edgeRadius);
        if (y > bounds.yMax - edgeRadius)
            steer += new Vector2(0, (edgeRadius - (bounds.yMax - y)) / -edgeRadius);*/
        //"digital" steering
        if (x < bounds.xMin + edgeRadius)
            steer += new Vector2(1, 0);
        if (x > bounds.xMax - edgeRadius)
            steer += new Vector2(-1, 0);
        if (y < bounds.yMin + edgeRadius)
            steer += new Vector2(0, 1);
        if (y > bounds.yMax - edgeRadius)
            steer += new Vector2(0, -1);

        //hard edge
        /*if (x < bounds.xMin)
            transform.position = new Vector3(bounds.xMin, transform.position.y, transform.position.z);
        else if (x > bounds.xMax)
            transform.position = new Vector3(bounds.xMax, transform.position.y, transform.position.z);
        if (y < bounds.yMin)
            transform.position = new Vector3(transform.position.x, bounds.yMin, transform.position.z);
        else if (y > bounds.yMax)
            transform.position = new Vector3(transform.position.x, bounds.yMax, transform.position.z);*/

        return steer;
    }

    private void eat(SwimmingCreature creature, float distSq)
    {
        if (distSq < eatSq)
        {
            creature.getEaten();
        }
    }

    public void getEaten()
    {
        //TODO: play death animation
        //TODO: don't spawn until animation is done playing
        Spawn();
    }

    public void Spawn()
    {
        //randomly spawn outside the bounds
        float xSpawn;
        float ySpawn;
        if (Random.value < .5)
            xSpawn = bounds.xMin + bounds.xMin * Random.value;
        else
            xSpawn = bounds.xMax + bounds.xMax * Random.value;

        xSpawn = Random.value * bounds.xMax;
        ySpawn = Random.value * bounds.yMax;

        transform.position = new Vector3(xSpawn, ySpawn, transform.position.z);
        velocity = new Vector2(Random.Range(-maxSpeed, maxSpeed),
            Random.Range(-maxSpeed, maxSpeed));
    }

    public void KillForever()
    {
        Destroy(gameObject);
    }
}
