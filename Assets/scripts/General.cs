using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class General : MonoBehaviour {

    public static float ConvertRange(
    float originalStart, float originalEnd, // original range
    float newStart, float newEnd, // desired range
    float value) // value to convert
    {
        double scale = (double) (newEnd - newStart) / (originalEnd - originalStart);
        return (float) (newStart + ((value - originalStart) * scale));
    }
}
