using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookDetector : MonoBehaviour
{

    GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hookable"))
        {
            player.GetComponent<GrapplingHook>().hooked = true;

            player.GetComponent<GrapplingHook>().hookedObj = other.gameObject;
        }
    }
}
