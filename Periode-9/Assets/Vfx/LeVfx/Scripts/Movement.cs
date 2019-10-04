using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public CharacterController cC;
    public float speed;
    float sped;
    Vector3 movement;

    public float jumpPower;
    public Transform jumpSource;
    bool canJump = true;

    public float dragStrength;

    public SkillActivator jumpSkill;

    public ParticleSystem walkingDust;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            sped = speed * 2.5f;
        }
        else
        {
            sped = speed;
        }

        movement.x = Input.GetAxis("Horizontal");
        movement.z = Input.GetAxis("Vertical");

        transform.Translate(transform.TransformDirection(movement)*sped,Space.World);

        /*if(cC.velocity.z > 0)
        {
            transform.LookAt(cC.velocity + transform.position, transform.up);
        }
        */

        if (canJump && Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Rigidbody>().AddForce(transform.up * jumpPower, ForceMode.Impulse);
            canJump = false;
        }

        if (canJump)
        {
            jumpSkill.outsideCanBeUsed = true;
        }
        else
        {
            jumpSkill.outsideCanBeUsed = false;
        }

        if(!canJump && Input.GetKey(KeyCode.LeftControl))
        {
            GetComponent<Rigidbody>().drag = dragStrength;
        }
        else
        {
            GetComponent<Rigidbody>().drag = 0;
        }
        
        if(canJump)
        {
            walkingDust.enableEmission = true;
        }
        else
        {
            walkingDust.enableEmission = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Ground")
        {
            canJump = true;
        }
    }
}
