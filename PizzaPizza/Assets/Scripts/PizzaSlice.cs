using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaSlice : MonoBehaviour
{
    public Vector3 target;
    public GameObject player;

    public float projectileSpeed = 0.001f;

    // Update is called once per frame
    void Update()
    {
        Vector3 distanceV = target - transform.position;
        distanceV = distanceV.normalized;
        if (distanceV.magnitude < projectileSpeed)
        {
            transform.position = target;
        }
        else
        {
            this.transform.position += distanceV * projectileSpeed;
        }

        if (this.gameObject.activeSelf == false) 
        {
            Destroy(this);
        }
    }
}
