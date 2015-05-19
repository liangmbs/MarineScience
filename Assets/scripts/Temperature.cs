using UnityEngine;
using System.Collections.Generic;

public class Temperature : MonoBehaviour {

    private LineRenderer historyLine;

    private List<LineRenderer> lines;
    private List<SpriteRenderer> dots; 

    public float maxTemp = 40;
    public float minTemp = 0;

    public float maxTempVariance = 10; //per day
    public float minTempVariance = 3;

    public float maxChanceDots = 8;
    public float dotSpreadChance = .5f;

    public int numPredictions = 4;

    public Color hotColor = Color.red;
    public Color neutralColor = Color.yellow;
    public Color coldColor = Color.blue;

    public GameObject linePrefab;
    public GameObject dotPrefab;

    public float dotSpacing = 3;
    public float segmentSpacing = 5;
    public int historicalSegments = 6;

    public float animationSeconds = 4; //how long the animation plays
    public float animationFlipSpeed = .1f; //how fast it flips between dots.
    private float animationTimer = 0;

    private Queue<float> historicalTemperatures;

    private List<float> possibleTemperatures;
    private List<int> possibleLikelihoods;

    public float temperature = 20;

	// Use this for initialization
	void Start () {
        historyLine = GetComponent<LineRenderer>();
        historicalTemperatures = new Queue<float>();
        for (int i = 0; i < historicalSegments; i++)
        {
            historicalTemperatures.Enqueue(20);
        }

        //create our branching lines
        lines = new List<LineRenderer>();
        for (int i = 0; i < numPredictions; i++)
        {
            //instantiate
            GameObject lineObj = (GameObject)UnityEditor.PrefabUtility.InstantiatePrefab(linePrefab);
            //make the lines children of this object
            lineObj.transform.parent = this.transform;
            //and put them in a list
            lines.Add(lineObj.GetComponent<LineRenderer>());
        }

        //create some dots
        dots = new List<SpriteRenderer>();
        for (int i = 0; i < numPredictions + maxChanceDots; i++)
        {
            GameObject dotObj = (GameObject)UnityEditor.PrefabUtility.InstantiatePrefab(dotPrefab);
            dotObj.transform.parent = this.transform;
            dots.Add(dotObj.GetComponent<SpriteRenderer>());
        }

        generatePossibilities();
        drawLines();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Jump"))
        {
            generatePossibilities();
            drawLines();
        }
	}

    //creates possible temperatures
    void generatePossibilities()
    {
        possibleTemperatures = new List<float>();
        possibleLikelihoods = new List<int>();

        //the first possibility is always "no change"
        possibleTemperatures.Add(temperature);
        possibleLikelihoods.Add(1);

        //randomly start either up or down
        bool goUp = Random.value < .5;
        float lastUp = temperature;
        float lastDown = temperature;

        //either add a new temperature above or below our current temperature
        //unless it's outside of our max/min temperature
        //also make sure to keep the slightly-randomized temperatures inside of our max/min
        for (int i = 1; i < numPredictions; i++)
        {
            if (goUp)
            {
                if (lastUp + minTempVariance < maxTemp)
                {
                    float newTemp = lastUp + minTempVariance + (maxTempVariance - minTempVariance) * Random.value;
                    newTemp = Mathf.Min(newTemp, maxTemp);
                    lastUp = newTemp;
                    possibleTemperatures.Add(newTemp);
                    possibleLikelihoods.Add(1);
                }
            }
            else
            {
                if (lastDown - minTempVariance > minTemp)
                {
                    float newTemp = lastDown - minTempVariance - (maxTempVariance - minTempVariance) * Random.value;
                    newTemp = Mathf.Max(newTemp, minTemp);
                    lastDown = newTemp;
                    possibleTemperatures.Add(newTemp);
                    possibleLikelihoods.Add(1);
                }
            }

            goUp = !goUp;
        }

        //add the likihood "dots" randomly.
        for (int i = 0; i < maxChanceDots; i++)
        {
            int index = Random.Range(0, possibleLikelihoods.Count);
            possibleLikelihoods[index] = possibleLikelihoods[index] + 1;
        }
    }

    //draw the lines (and dots)
    void drawLines()
    {
        float x = transform.position.x;
        historyLine.SetVertexCount(historicalSegments);

        for (int i = 0; i < lines.Count; i++)
        {
            if (i > possibleTemperatures.Count)
            {
                //we don't need this line, so hide it.
                lines[i].enabled = false;
            }
            else
            {
                LineRenderer lin = lines[i];
                lin.enabled = true;
                lin.SetVertexCount(2);
                lin.SetPosition(0, new Vector3(segmentSpacing, temperature, i));
                lin.SetPosition(1, new Vector3(segmentSpacing * 2, possibleTemperatures[i], i));
                lin.SetColors(getColorTemperature(temperature), getColorTemperature(possibleTemperatures[i]));
            }
        }

        int d = 0;
        Debug.Log("dots " + dots.Count);
        for (int i = 0; i < possibleLikelihoods.Count; i++)
        {
            int nDots = possibleLikelihoods[i];
            Color cTemp = getColorTemperature(possibleTemperatures[i]);
            float xOrigin = segmentSpacing * 2 + dotSpacing;
            float yPos = possibleTemperatures[i];
            for (int j = 0; j < nDots; j++)
            {
                Debug.Log("d" + d);
                SpriteRenderer dot = dots[d];
                dot.color = cTemp;
                dot.transform.position = new Vector3(xOrigin + dotSpacing * j, yPos, 0);
                d++;
            }
        }
    }

    Color getColorTemperature(float temp)
    {
        float mid = (minTemp + maxTemp) / 2;
        float div = maxTemp - mid;
        if (temp == mid)
        {
            return neutralColor;
        }
        else if (temp > mid)
        {
            return Color.Lerp(neutralColor, hotColor, (temp - mid) / div);
        }
        else
        {
            return Color.Lerp(coldColor, neutralColor, (temp - minTemp) / div);
        }
    }
}
