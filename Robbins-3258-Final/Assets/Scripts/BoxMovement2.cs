using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMovement2 : MonoBehaviour
{
    bool notMoving;
    public float speed = .5f;
    void Start()
    {
        notMoving = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (notMoving)
        {
            StartCoroutine(movement());
        }
    }

    IEnumerator movement()
    {
        notMoving = false;

        for (int i = 0; i < 50; i++)
        {
            transform.position += Vector3.up * .1f;
            yield return new WaitForSeconds(.05f);
        }

        for (int i = 0; i < 50; i++)
        {
            transform.position -= Vector3.up * .1f;
            yield return new WaitForSeconds(.05f);
        }

        notMoving = true;
    }
}
