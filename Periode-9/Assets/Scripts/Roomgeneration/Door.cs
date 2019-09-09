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
    public bool active;
    public Animator switchScreen;

    public void Update()
    {
        if (Physics.CheckBox(transform.position, playerDetectArea, Quaternion.identity, playerMask) && !active && roomLayout.RoomClearInfo())
        {
            CharacterMovement movement = GameObject.FindWithTag("Player").GetComponent<CharacterMovement>();
            active = true;
            movement.allowMovement = false;
            StartCoroutine(MovePlace(movement));
        }
    }

    public IEnumerator MovePlace(CharacterMovement player)
    {
        switchScreen.SetTrigger("SwitchScreen");
        yield return new WaitForSeconds(0.27f);
        StartCoroutine(player.ParticleTempDisable());
        player.allowMovement = true;
        player.StartCoroutine(player.StartMovement(0.27f));
        active = false;
        roomLayout.currentlyLocated += addValue;
        player.transform.position = returnPoint.position;
        roomLayout.DisplayRoom();
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, playerDetectArea);
    }
}
