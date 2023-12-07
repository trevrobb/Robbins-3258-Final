using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingHook : MonoBehaviour
{
    [SerializeField] GameObject hook;
    [SerializeField] GameObject hookHolder;

    [SerializeField] float hookTravelSpeed;
    [SerializeField] float playerTravelSpeed;

    public static bool fired;
    public bool hooked;
    public GameObject hookedObj;
    [SerializeField] float maxDistance;
    private float currentDistance; 



    
    void Start()
    {
        hook.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && !fired)
        {
            fired = true;
        }

        if (fired && !hooked)
        {
            hook.SetActive(true);
            hook.transform.Translate(Vector3.forward * Time.deltaTime * hookTravelSpeed);
            currentDistance = Vector3.Distance(transform.position, hook.transform.position);

            if (currentDistance >= maxDistance)
            {
                ReturnHook();
            }
        }

        if (hooked)
        {
            hook.transform.parent = hookedObj.transform;
            transform.position = Vector3.MoveTowards(transform.position, hook.transform.position, playerTravelSpeed * Time.deltaTime);
            float distanceToHook = Vector3.Distance(transform.position, hook.transform.position);

            this.GetComponent<Rigidbody>().useGravity = false;
            if (distanceToHook < 2)
            {
                ReturnHook();
            }
        }
        else
        {
            hook.transform.parent = hookHolder.transform;
            this.GetComponent<Rigidbody>().useGravity = true;
        }
    }

    void ReturnHook()
    {
        
        hook.transform.position = hookHolder.transform.position;
        hook.SetActive(false);
        fired = false;
        hooked = false; 
    }
}
