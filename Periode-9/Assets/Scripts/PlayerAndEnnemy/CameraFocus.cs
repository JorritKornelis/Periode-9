using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFocus : MonoBehaviour
{
    public CameraPositions originalPos;
    public Vector3 otherPosOffset;
    public Vector3 otherAreaPos, otherAreaSize;
    public LayerMask playerMask;
    public List<CameraPositions> positions;
    public float lerpSpeed;
    public int current = -1;
    public float stopDistance;
    public bool test;
    public int testIndex;
    public bool reset, active;

    public void Update()
    {
        if (Physics.CheckBox(otherAreaPos, otherAreaSize, Quaternion.identity, playerMask) && GetComponent<CameraPlayerFollow>().enabled)
            transform.position = Vector3.Lerp(transform.position, originalPos.pos + otherPosOffset, Time.deltaTime * lerpSpeed);
        else if (GetComponent<CameraPlayerFollow>().enabled)
            transform.position = Vector3.Lerp(transform.position, originalPos.pos, Time.deltaTime * lerpSpeed);
        if (test)
        {
            StartCoroutine(MoveTowardsPoint(testIndex));
            test = false;
        }
    }

    public void Start()
    {
        originalPos = new CameraPositions(transform.position, Camera.main.fieldOfView, Vector3.zero);
    }

    public IEnumerator MoveTowardsPoint(int index)
    {
        active = true;
        if (index >= 0)
            GetComponent<CameraPlayerFollow>().enabled = false;
        else
            GetComponent<CameraPlayerFollow>().enabled = true;
        Vector3 pos = (index >= 0) ? positions[index].pos : originalPos.pos;
        Vector3 pointOfInterest = (index >= 0) ? positions[index].pointOfInterest : originalPos.pointOfInterest;
        float fov = (index >= 0) ? positions[index].fov : originalPos.fov;
        yield return null;
        while (Vector3.Distance(transform.position,pos) > stopDistance)
        {
            if (reset)
            {
                reset = false;
                break;
            }
            transform.position = Vector3.Lerp(transform.position, pos, Time.deltaTime * lerpSpeed);
            Camera.main.fieldOfView = Mathf.Lerp(Camera.main.fieldOfView, fov, Time.deltaTime * lerpSpeed);
            if (index >= 0)
                transform.LookAt(pointOfInterest);
            yield return null;
        }
        active = false;
    }

    public void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position + otherPosOffset, 0.3f);
        Gizmos.DrawWireCube(otherAreaPos, otherAreaSize * 2f);
        float colorAmount = 1 / positions.Count;
        for (int i = 0; i < positions.Count; i++)
        {
            Gizmos.color = new Color(colorAmount * i, 0, colorAmount * i);
            Gizmos.DrawSphere(positions[i].pos, 0.3f);
            Gizmos.DrawLine(positions[i].pos, positions[i].pointOfInterest);
        }
    }
}

[System.Serializable]
public class CameraPositions
{
    public Vector3 pointOfInterest;
    public Vector3 pos;
    public float fov;
    public CameraPositions(Vector3 _pos, float _fov, Vector3 _pointOfInterest)
    {
        pos = _pos;
        fov = _fov;
        pointOfInterest = _pointOfInterest;
    }
}
