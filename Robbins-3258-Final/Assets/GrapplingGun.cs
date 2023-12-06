using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingGun : MonoBehaviour
{
   

    private LineRenderer lr;
    private Vector3 grapplePoint;
    [SerializeField] LayerMask grappleObject;
    public Transform gunTip, camera, player; //Cannot figure out how to not make this one public
    float maxDistance = 100f;
    [SerializeField] float grappleDelayTime;
    SpringJoint joint;
    [SerializeField] PlayerMovement _player;
    private Boolean isSwinging;
    private Boolean grappling;
    [SerializeField] float overshootYAxis;

    [SerializeField] float grapplingCd;
    private float grapplingCdTimer;

    [SerializeField] KeyCode grappleKey = KeyCode.Mouse1;


    [SerializeField] Transform orientation;
    [SerializeField] Rigidbody rb;
    [SerializeField] float horinontalThrustForce;
    [SerializeField] float forwardThrustForce;
    [SerializeField] float extendCableSpeed;

    [SerializeField] RaycastHit predictionHit;
    [SerializeField] float predictionSphereCastRadius;
    [SerializeField] Transform predictionPoint;
    public static GrapplingGun instance;
    [SerializeField] AudioClip swing;
    [SerializeField] AudioClip grap;



    private void Awake()
    {
        _player = GetComponent<PlayerMovement>();
        instance = this;
    }




    void Update()
    {
        

        if (Input.GetKeyDown(grappleKey))
        {

            StartGrappling();
        }

        if (grapplingCdTimer > 0)
            grapplingCdTimer -= Time.deltaTime;

        

        checkForSwingPoint();
    }


  
    public Vector3 getGrapplePoint()
    {
        return grapplePoint;
    }

    public Boolean isGrapple()
    {
        return isSwinging;
    }
    public Boolean isGrapplingg()
    {
        return grappling;
    }

    private void StartGrappling()
    {
        if (grapplingCdTimer > 0) return;
        grappling = true;
        
        RaycastHit hit;
        if (predictionHit.point != Vector3.zero)
        {
            grapplePoint = predictionHit.point;
            Invoke(nameof(ExecuteGrapple), grappleDelayTime);

        }
        else
        {
            grapplePoint = camera.position + camera.forward * maxDistance;
            Invoke(nameof(StopGrappling), grappleDelayTime);
        }
    }

    private void ExecuteGrapple()
    {

        Vector3 lowestPoint = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);
        float grapplePointRelativeYPos = grapplePoint.y - lowestPoint.y;
        float highestPointOnArc = grapplePointRelativeYPos + overshootYAxis;

        if (grapplePointRelativeYPos < 0) highestPointOnArc = overshootYAxis;
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>().JumpToPosition(grapplePoint, highestPointOnArc);
        Invoke(nameof(StopGrappling), 0.8f);



    }
    public void StopGrappling()
    {
        grappling = false;
        grapplingCdTimer = grapplingCd;
    }
    //Movement script that allows you to do a lot more with your swinging, called odmMovement as a nod to Attack on Titan :)
 
    private void checkForSwingPoint()
    {
        if (joint != null) return;

        RaycastHit sphereCastHit;
        Physics.SphereCast(Camera.main.transform.position, predictionSphereCastRadius, Camera.main.transform.forward, out sphereCastHit, maxDistance, grappleObject);

        RaycastHit raycastHit;
        Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out raycastHit, maxDistance, grappleObject);

        Vector3 realHitPoint;
        if (raycastHit.point != Vector3.zero)
        {
            realHitPoint = raycastHit.point;
        }
        else if (sphereCastHit.point != Vector3.zero)
        {
            realHitPoint = sphereCastHit.point;
        }
        else
        {
            realHitPoint = Vector3.zero;
        }
        if (realHitPoint != Vector3.zero)
        {
            predictionPoint.gameObject.SetActive(true);
            predictionPoint.position = realHitPoint;


        }
        else
        {
            predictionPoint.gameObject.SetActive(false);
        }

        predictionHit = raycastHit.point == Vector3.zero ? sphereCastHit : raycastHit;

    }





}