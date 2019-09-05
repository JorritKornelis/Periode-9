using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public LayerMask playerMask;
    public Transform returnPoint;
    public Vector2Int addValue;
    public RoomLayoutGeneration roomLayout;
    public Vector3 playerDetectArea;

    public void Update()
    {
        if (Physics.CheckBox(transform.position, playerDetectArea, Quaternion.identity, playerMask))
        {
            StartCoroutine(GameObject.FindWithTag("Player").GetComponent<CharacterMovement>().ParticleTempDisable());
            roomLayout.currentlyLocated += addValue;
            roomLayout.DisplayRoom();
            GameObject.FindWithTag("Player").transform.position = returnPoint.position;
        }  
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, playerDetectArea);
    }
}
