using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiagonalMove2 : MonoBehaviour
{
    bool notMoving;
    public float speed = .5f;

    public float delay;
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
        yield return new WaitForSeconds(delay);
        for (int i = 0; i < 50; i++)
        {
            transform.position += new Vector3(1f, 1f, 0f).normalized * .01f;
            yield return new WaitForSeconds(.05f);
        }

        for (int i = 0; i < 50; i++)
        {
            transform.position -= new Vector3(1f, 1f, 0f).normalized * .01f;
            yield return new WaitForSeconds(.05f);
        }

        notMoving = true;
    }
}
