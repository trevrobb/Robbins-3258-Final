using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingGun : MonoBehaviour
{


    private PlayerMovement pm;
    public Transform cam;
    public Transform gunTip;
    public LayerMask grappleMask;

    public float maxGrappleDistance;
    public float grappleDelayTime;

    public LineRenderer lineRenderer;
    private Vector3 grapplePoint;

    public float grapplingCd;
    private float grapplingCdTimer;

    public KeyCode grapplingKey = KeyCode.LeftControl;

    public bool grappling;

    public static GrapplingGun instance;

    public float overShootYAxis;

    private void Start()
    {
        pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(grapplingKey)) StartGrapple();

        if (grapplingCdTimer > 0)
        {
            grapplingCdTimer -= Time.deltaTime;
        }
    }

    private void LateUpdate()
    {
        if (grappling)
        {
            lineRenderer.SetPosition(0, gunTip.position);
        }
    }
    private void StartGrapple()
    {
        if (grapplingCdTimer > 0) return;
        grappling = true;
        if (grappling)
        {
            lineRenderer.SetPosition(0, gunTip.position);
        }
        RaycastHit hit;

        pm._freeze = true;
        if (Physics.Raycast(cam.position, cam.forward, out hit, maxGrappleDistance, grappleMask)){
            grapplePoint = hit.point;
            
            Invoke(nameof(ExecuteGrapple), grappleDelayTime);
        }
        else
        {
            grapplePoint = cam.position + cam.forward * maxGrappleDistance;

            Invoke(nameof(StopGrapple), grappleDelayTime);
        }

        lineRenderer.enabled = true;

        lineRenderer.SetPosition(1, grapplePoint);
       
    }

    private void ExecuteGrapple()
    {
        pm._freeze = false;

        Vector3 lowestPoint = new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z);

        float grapplePointRelativeYPos = grapplePoint.y - lowestPoint.y;

        float highestPointOnArc = grapplePointRelativeYPos + overShootYAxis;

        if (grapplePointRelativeYPos < 0) highestPointOnArc = overShootYAxis;

        pm.JumpToPosition(grapplePoint, highestPointOnArc);

        Invoke(nameof(StopGrapple), 1f);
    }

    public void StopGrapple()
    {
        grappling = false;
        grapplingCdTimer = grapplingCd;

        lineRenderer.enabled = false;
    }

    public Vector3 getGrapplePoint()
    {
        return grapplePoint;
    }


}