﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    Rigidbody rb;
    Transform t;
    [SerializeField]
    float speed = 5;
    bool isDestroyed = false;
    private void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        t = transform;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = Vector3.left * speed;
    }
    
    public void initPosit(Vector3 position)
    {
        t.position = position;
        rb.velocity = Vector3.zero;
    }
    public Vector3 getLoc()
    {
        return t.position;
    }
    public bool isDead()
    {
        return isDestroyed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cannonball"))
        {
            isDestroyed = true;
            Debug.Log("I'm Dead");
        }
    }
    public void suicide()
    {
        Destroy(gameObject);
    }
}
