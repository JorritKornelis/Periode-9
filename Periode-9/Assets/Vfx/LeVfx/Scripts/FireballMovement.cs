using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]

public class FireballMovement : MonoBehaviour
{
    public float speed = 1;
    public GameObject explosion;
    public float explosionDuration = 10;
    public Vector3 direction;

    public ParticleSystem[] particlesToStop;

    private void Awake()
    {
        transform.SetParent(null);
    }

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Ground")
        {
            GetComponent<SphereCollider>().enabled = !true;
            speed = 0;
            gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
            if(particlesToStop.Length != 0)
            {
                for (int i = 0; i < particlesToStop.Length; i++)
                {
                    particlesToStop[i].Stop();
                }
            }
            print("Hit");
            GameObject g = Instantiate(explosion, transform);
            Destroy(g, explosionDuration);
            Destroy(transform.root.gameObject, explosionDuration + 0.1f);
        }
    }
}
