using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LooseMovingObject : LooseObject
{

    public RigidbodyConstraints startingConstraints;
    // Start is called before the first frame update
    public override void Break()
    {
        Debug.Log("player");
        broken = true;
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        startingConstraints = rb.constraints;
        rb.constraints = RigidbodyConstraints.None;
    }

    public override void Respawn()
    {
        transform.position = respawnPoint;
        transform.rotation = respawnRotation;
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();
        rb.constraints = startingConstraints;
        rb.velocity = Vector3.zero;
        despawnTimer = despawnTime;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
