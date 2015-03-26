using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class dataChart : MonoBehaviour {

    public float yOffset = 0f;
    public int dataCrop = 100;
    public float[] data;
    public Color color;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {

	}

    void OnDrawGizmos()
    {
        
    }

    public void setData(float[] dat)
    {
        data = new float[(int) (dat.Length / dataCrop) - 1];
        for (int i = 0; i < (dat.Length / dataCrop) - 1; i++)
        {
            data[i] = dat[i * dataCrop];
        }

        LineRenderer renderer = GetComponent<LineRenderer>();
        if (renderer == null)
        {
            Debug.Log("please attach a linerenderer");
        }
        renderer.SetVertexCount(data.Length);

        float x = gameObject.transform.position.x;
        float y = gameObject.transform.position.y;

        for (int i = 0; i < data.Length; i++)
        {
            renderer.SetPosition(i, new Vector3(i * transform.localScale.x,
                data[i] * transform.localScale.y + yOffset * transform.localScale.y, 0));
        }
    }
}
