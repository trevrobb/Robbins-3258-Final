using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shootable : MonoBehaviour
{
    public int health = 3;

    public void Damage(int damageAmount)
    {
        health -= damageAmount;

        if (health <= 0)
        {
            GameObject.Destroy(gameObject);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
