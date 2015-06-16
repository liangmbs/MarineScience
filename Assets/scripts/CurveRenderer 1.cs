using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class curveRenderer : MonoBehaviour {

    //public Color hotColor;
    //public Color neutralColor;
    //public Color coldColor;
    public ThermalCurve curve;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {

	}

    public void updateChart()
    {
        Vector3 pos = transform.position;
        LineRenderer line = GetComponent<LineRenderer>();
        line.SetVertexCount(41);
        for (int i = 0; i <= 40; i++)
        {
            line.SetPosition(i, transform.position);
        }
    }
}
