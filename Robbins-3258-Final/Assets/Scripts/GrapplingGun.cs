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

    private bool grappling;

    private void Start()
    {
        pm = GetComponent<PlayerMovement>();
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

        RaycastHit hit;

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

    }

    private void StopGrapple()
    {
        grappling = false;
        grapplingCdTimer = grapplingCd;

        lineRenderer.enabled = false;
    }


}