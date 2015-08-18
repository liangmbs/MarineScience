using UnityEngine;
using System.Collections;

public class FishDrop {

    Vector3 end;

    static float curvePower = 2;
    float startYAdd = 100;
    public static float sceneHeight = 50;

    public FishDrop(Vector3 endPos, float falldistance)
    {
        end = endPos;
        startYAdd = falldistance;
    }

    public Vector3 getPos(float time)
    {
        if (time == 0)
        {
            return end + new Vector3(0, startYAdd, 0);
        }
        return end + new Vector3(0, startYAdd * (Mathf.Pow(time, curvePower) / Mathf.Pow(1, curvePower)), 0);
        //return start + new Vector3(0, endPosAdd * (time / 1), 0);
    }
}
