using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class CharacterMovement : MonoBehaviour
{
    [Header("MovementSpeeds")]
    public float moveSpeed;
    public float fallSpeed;
    public float fallVelocity;
    public float accelAmount;
    public float rotateLerpSpeed;
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
    public ParticleSystem walkDust;
    Inventory invetoryHolder;
    public bool allowMovement;
    float currentAccel;
    Vector3 currentMovementSpeed;
    [Header("Other")]
    public bool inTheChestBool = false;
    public bool inDungeonAnimations;
    public Animator animator;
    public string interactionInput;
    public LayerMask sellTableMask;
    public bool inSellPoint;

    public IEnumerator StartMovement(float time)
    {
        yield return new WaitForSeconds(time);
        allowMovement = true;
    }

    private void Start()
    {
        invetoryHolder = GetComponent<Inventory>();
    }

    public IEnumerator ParticleTempDisable()
    {
        float f = 0.01f;
        while (f > 0)
        {
            f -= Time.deltaTime;
            walkDust.gameObject.SetActive(false);
            yield return null;
        }
    }

    public void Update()
    {
        if (allowMovement)
            Move();
        if (Input.GetButtonDown(interactionInput))
            CheckForTable();
    }

    public void CheckForTable()
    {
        CameraFocus focus = Camera.main.transform.parent.GetComponent<CameraFocus>();
        if (!inSellPoint)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, pickUpRadis, sellTableMask);
            if (colliders.Length > 0)
            {
                if (focus.active)
                    focus.reset = true;
                SellingTable table = colliders[0].GetComponent<SellingTable>();
                table.InteractionStart();
                StartCoroutine(focus.MoveTowardsPoint(table.cameraLoc));
                Inventory inv = GetComponent<Inventory>();
                inv.inv.SetActive(true);
                allowMovement = false;
                inSellPoint = true;
            }
        }
        else
        {
            if (focus.active)
                focus.reset = true;
            StartCoroutine(focus.MoveTowardsPoint(-1));
            Inventory inv = GetComponent<Inventory>();
            inv.inv.SetActive(false);
            allowMovement = true;
            inSellPoint = false;
        }
    }

    public void Move()
    {
        currentAccel = Mathf.Lerp(currentAccel, (Input.GetButton("Horizontal") || Input.GetButton("Vertical")) ? 1 : 0, Time.deltaTime * accelAmount);
        Vector3 currentMove = new Vector3(GetCollisionMoveAmount(Vector3.right, Input.GetAxis("Horizontal")), 0, GetCollisionMoveAmount(Vector3.forward, Input.GetAxis("Vertical"))).normalized * moveSpeed * currentAccel;
        currentMovementSpeed = Vector3.Lerp(currentMovementSpeed, currentMove, Time.deltaTime * accelAmount);
        if (GetCollisionMoveAmount(Vector3.right, Input.GetAxis("Horizontal")) == 0 && Input.GetButton("Horizontal"))
            currentMovementSpeed.x = 0;
        if (GetCollisionMoveAmount(Vector3.forward, Input.GetAxis("Vertical")) == 0 && Input.GetButton("Vertical"))
            currentMovementSpeed.z = 0;
        CheckCollisionPickUp();
        if (Vector3.Distance(Vector3.zero, currentMovementSpeed) > 0.2f)
        {
            Vector3 animationDirection = body.InverseTransformDirection(currentMovementSpeed.normalized);
            animator.SetFloat("Horizontal", animationDirection.x);
            animator.SetFloat("Vertical", animationDirection.z);
        }
        else
        {
            animator.SetFloat("Horizontal", 0);
            animator.SetFloat("Vertical", 0);
        }
        if (GroundCheck())
        {
            CursorFollow();
            walkDust.gameObject.SetActive(true);
            CheckForNewSavePoint();
            transform.Translate(currentMovementSpeed * Time.deltaTime);
        }
        else
        {
            walkDust.gameObject.SetActive(false);
            fallVelocity += fallSpeed * Time.deltaTime;
            transform.Translate(Vector3.down * Time.deltaTime * fallVelocity);
            if (transform.position.y < resetHeight)
            {
                transform.position = lastSaveSpot;
                fallVelocity = 0;
                currentMovementSpeed = Vector3.zero;
                StartCoroutine(Camera.main.GetComponent<ScreenShake>().Shake(0.3f));
            }
        }
    }

    public void CursorFollow()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, cursorMask))
        {
            var targetRotation = Quaternion.LookRotation(new Vector3(hit.point.x, body.position.y, hit.point.z) - body.position);
            body.rotation = Quaternion.Lerp(body.rotation, targetRotation, Time.deltaTime * rotateLerpSpeed);
        }
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
            if (Physics.Raycast(transform.position + (sideDirection * raycastOffset * i), direction, raycastRange, obstacleMask))
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
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, pickUpRadis, itemLayer);
        int i = 0;
        while (i < hitColliders.Length)
        {            
            if(hitColliders[i].gameObject.GetComponent<ItemIndex>() && hitColliders[i].gameObject.GetComponent<ItemIndex>().mayAdd)
            {
                GetComponent<Inventory>().AddItem(hitColliders[i].gameObject.GetComponent<ItemIndex>().index, hitColliders[i].gameObject.GetComponent<ItemIndex>().amoundInItem);
                Destroy(hitColliders[i].gameObject);
            }
            
            i++;
        }
        inTheChestBool = false;
    }
}
