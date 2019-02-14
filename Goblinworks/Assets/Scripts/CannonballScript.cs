using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonballScript : MonoBehaviour
{
    Vector3 mTarget = Vector3.zero;
    [SerializeField]
    float speed = 0.5f;
    [SerializeField]
    float explodeRadius = 0.2f; //distance from target where ball explodes

    float deathTimer = 0.0f; // if in the air for larger than 3s destroy
    float lifetime = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
    }

    private void Update()
    {
        deathTimer += Time.deltaTime;
        if (deathTimer >= lifetime)
        {
            deathTimer = 0;
            Destroy(gameObject);
        }
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
