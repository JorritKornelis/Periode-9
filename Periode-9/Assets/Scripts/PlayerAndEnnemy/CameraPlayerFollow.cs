using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayerFollow : MonoBehaviour
{
    public string playerTag;
    Transform player;
    [Range(0,1)]
    public float playerFollowAmount = 0.5f;
    Vector3 lastplayerpos;
    public float lerpSpeed = 5;
    public float mouseAmount = 2;

    public void Start()
    {
        player = GameObject.FindWithTag(playerTag).transform;
    }

    public void Update()
    {
        Vector3 mousePosInScreen = new Vector3(Input.mousePosition.x / Screen.width - 0.5f, 0, Input.mousePosition.y / Screen.height - 0.5f);
        transform.LookAt(Vector3.Lerp(Vector3.zero, new Vector3(lastplayerpos.x, 0, lastplayerpos.z),playerFollowAmount));
        lastplayerpos = Vector3.Lerp(lastplayerpos, player.position + (mousePosInScreen * mouseAmount),Time.deltaTime * lerpSpeed);
    }
}
