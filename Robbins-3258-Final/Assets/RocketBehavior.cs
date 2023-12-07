using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBehavior : MonoBehaviour
{
    Rigidbody rb;
    Vector3 moveDir;
    float speed = 10f;

    public float explosionForce = 50f;
    public float explosionRadius = 5f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        moveDir = Camera.main.transform.forward * 25f - this.transform.forward;
        RocketBehavior r = GetComponent<RocketBehavior>();

        StartCoroutine(Despawn());

    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(moveDir);
    }

    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(1.2f);
        Destroy(this);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Rigidbody playerRB = other.gameObject.GetComponent<Rigidbody>();
            if (playerRB != null)
            {
                Vector3 forceDirection = (other.transform.position - transform.position).normalized;

               

                playerRB.AddForce(forceDirection * explosionForce, ForceMode.Impulse);
            }
        }
        Explode();
        Destroy(gameObject);
    }

    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 forceDirection = (hit.transform.position - transform.position).normalized;

                rb.AddForce(forceDirection * explosionForce, ForceMode.Impulse);
            }
        }
        Destroy(gameObject);
    }


}
