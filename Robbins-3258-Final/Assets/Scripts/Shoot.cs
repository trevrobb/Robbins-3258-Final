using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] int gunDamage = 1;
    [SerializeField] float fireRate = .25f;
    [SerializeField] float weaponRange = 50f;
    [SerializeField] float hitForce = 100f;
    [SerializeField] Transform gunEnd;
    [SerializeField] AudioSource gunSource;
    [SerializeField] ParticleSystem flash;
    private Camera cam;
    private WaitForSeconds shotDuration = new WaitForSeconds(.07f);
    private LineRenderer gunshotLine;
    private float nextFire;


    void Start()
    {
        
        cam = Camera.main;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;

            
            flash.Play();
            gunSource.Play();
            Vector3 rayOrigin = cam.ViewportToWorldPoint(new Vector3(.5f, .5f, 0));

            RaycastHit hit;

            if (Physics.Raycast(rayOrigin, cam.transform.forward, out hit, weaponRange))
            {
                
                Shootable shootableHealth = hit.collider.GetComponent<Shootable>();
                if (shootableHealth != null)
                {
                    shootableHealth.Damage(gunDamage);
                    
                }
            }
            
        }
    }

    
}
