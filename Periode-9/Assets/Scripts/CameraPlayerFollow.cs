using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPlayerFollow : MonoBehaviour
{
    public string playerTag;
    Transform player;
    [Range(0,1)]
    public float playerFollowAmount = 0.5f;

    public void Start()
    {
        player = GameObject.FindWithTag(playerTag).transform;
    }

    public void Update()
    {
        transform.LookAt(Vector3.Lerp(Vector3.zero, new Vector3(player.position.x, 0, player.position.z),playerFollowAmount));
    }
}
