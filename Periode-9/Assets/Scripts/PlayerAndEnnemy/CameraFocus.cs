using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFocus : MonoBehaviour
{
    public CameraPositions originalPos;
    public List<CameraPositions> positions;
    public float lerpSpeed;
    public int current = -1;
}

[System.Serializable]
public class CameraPositions
{
    public Vector3 pos;
    public float fOV;
}
