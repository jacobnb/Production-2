﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonballScript : MonoBehaviour
{
    Vector3 mTarget = Vector3.zero;
    [SerializeField]
    float speed = 0.5f;
    float explodeRadius = 0.1f; //distance from target where ball explodes

    // Start is called before the first frame update
    void Start()
    {
    }

    private void FixedUpdate()
    {
        moveTowardsTarget();
        
    }

    void moveTowardsTarget()
    {
        Vector3 direction = mTarget - transform.position;
        transform.position += direction.normalized * speed;
        if(direction.magnitude <= explodeRadius)
        {
            explode();
        }
    }


    public void setTargetAndPosit(Vector3 target, Vector3 posit)
    {
        mTarget = target;
        transform.position = posit;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            explode();
        }
    }

    void explode()
    {
        transform.localScale = new Vector3(2.0f, 2.0f, 2.0f);
        Destroy(gameObject);
    }
}
