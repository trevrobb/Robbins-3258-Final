using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBehavior : MonoBehaviour
{
    Rigidbody rb;
    Vector3 moveDir;
    float speed = 10f;
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
        if (other.CompareTag("Player"))
        {
            
            other.GetComponent<Rigidbody>().AddExplosionForce(100f, transform.position, 3.0f, 2.5f, ForceMode.Impulse);
        }
    }
}
