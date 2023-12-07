using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketShoot : MonoBehaviour
{

    [SerializeField] int gunDamage = 1;
    [SerializeField] float fireRate = .25f;
    [SerializeField] float weaponRange = 50f;
    [SerializeField] float hitForce = 100f;
    [SerializeField] Transform gunEnd;
    [SerializeField] AudioSource gunSource;
    [SerializeField] ParticleSystem flash;
    [SerializeField] GameObject Rocket;
    private GameObject spawnedRocket;
    private Camera cam;
    private float nextFire;
    // Start is called before the first frame update
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


            //flash.Play();
            //gunSource.Play();
            Instantiate(Rocket, gunEnd);
            

            

        }
    }
}
