using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [Header("MovementSpeeds")]
    public float moveSpeed;
    public float fallSpeed;
    public float fallVelocity;
    [Header("Collisions")]
    public LayerMask obstacleMask;
    public float raycastRange;
    public float raycastOffset;
    Vector3 lastSaveSpot;
    public float resetHeight;
    public LayerMask itemLayer;
    public float pickUpRadis;
    [Header("CursorFollow")]
    public Transform body;
    public LayerMask cursorMask;
    public Inventory invetoryHolder;

    public void Update()
    {
        CursorFollow();
        CheckCollisionPickUp();
        if (GroundCheck())
        {
            CheckForNewSavePoint();
            transform.Translate(new Vector3(GetCollisionMoveAmount(Vector3.right, Input.GetAxis("Horizontal")), 0, GetCollisionMoveAmount(Vector3.forward, Input.GetAxis("Vertical"))) * moveSpeed * Time.deltaTime);
        }
        else
        {
            fallVelocity += fallSpeed * Time.deltaTime;
            transform.Translate(Vector3.down * Time.deltaTime * fallVelocity);
            if(transform.position.y < resetHeight)
            {
                transform.position = lastSaveSpot;
                fallVelocity = 0;
            }
        }
    }

    public void CursorFollow()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, cursorMask))
            body.LookAt(new Vector3(hit.point.x, body.position.y, hit.point.z));
    }

    public void CheckForNewSavePoint()
    {
        bool save = true;
        for (int x = -1; x <= 1; x++)
            for (int z = -1; z <= 1; z++)
                if (!Physics.Raycast(transform.position + (new Vector3(x, 0, z) * raycastOffset), Vector3.down, 1, obstacleMask))
                    save = false;
        if (save)
            lastSaveSpot = transform.position;
    }

    public float GetCollisionMoveAmount(Vector3 direction, float amount)
    {
        return CheckCollision(direction * (amount / Mathf.Abs(amount))) ? 0 : amount;
    }

    public bool GroundCheck()
    {
        for (int x = -1; x <= 1; x++)
            for (int z = -1; z <= 1; z++)
                if (Physics.Raycast(transform.position + (new Vector3(x, 0, z) * raycastOffset), Vector3.down, 1, obstacleMask))
                    return true;
        return false;
    }

    public bool CheckCollision(Vector3 direction)
    {
        Vector3 sideDirection = new Vector3(direction.z, 0, direction.x);
        for (int i = -1; i <= 1; i++)
            if(Physics.Raycast(transform.position + (sideDirection * raycastOffset * i), direction, raycastRange, obstacleMask))
                return true;
        return false;
    }

    public void OnDrawGizmos()
    {
        for (int i = -1; i <= 1; i++)
        {
            Gizmos.DrawRay(transform.position + (Vector3.right * raycastOffset * i), Vector3.forward * raycastRange);
            Gizmos.DrawRay(transform.position + (Vector3.right * raycastOffset * i), -Vector3.forward * raycastRange);

            Gizmos.DrawRay(transform.position + (Vector3.forward * raycastOffset * i), Vector3.right * raycastRange);
            Gizmos.DrawRay(transform.position + (Vector3.forward * raycastOffset * i), -Vector3.right * raycastRange);
        }
        for (int x = -1; x <= 1; x++)
            for (int z = -1; z <= 1; z++)
                Gizmos.DrawRay(transform.position + (new Vector3(x, 0, z) * raycastOffset), Vector3.down);
    }

    void CheckCollisionPickUp()
    {
        if (Physics.CheckSphere(transform.position, pickUpRadis, itemLayer))
        {
            Debug.Log("Collison whit pickup"); ///ref naar coll item??///
            //add to inv
            for (int i = 0; i < invetoryHolder.slotInformationArray.Length; i++)
            {
                if (invetoryHolder.slotInformationArray[i].slotImage.sprite == null)
                {
                    //pickUp info add to array
                    
                    break;
                }
            }
        }
    }
}
