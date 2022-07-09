using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LooseObject : MonoBehaviour
{
    [Tooltip("The vehicle layer")]
    public string layerName;
    [Tooltip("the velocity needed to knock down this object")]
    public float breakVelocity;
    [Tooltip("If the object is broken yet")]
    public bool broken;
    [Tooltip("if this object despawns or not")]
    public bool despawns;
    [Tooltip("how long it takes for this object to despawn if ever")]
    public float despawnTime;

    void OnCollisionEnter(Collision col)
    {
        if (broken)
        {
            return;
        }
        Debug.Log("collided");
        foreach (ContactPoint contact in col.contacts)
        {
            //Debug.Log(vehicleLayer);
            if(contact.otherCollider.gameObject.layer == LayerMask.NameToLayer(layerName) && col.relativeVelocity.magnitude > breakVelocity)
            {
                Debug.Log("player");
                broken = true;
                Rigidbody rb = gameObject.AddComponent<Rigidbody>();
                rb.constraints = RigidbodyConstraints.None;
            }
        }
            
    }

     void FixedUpdate()
    {
        if(broken && despawns)
        {
            despawnTime -= Time.fixedDeltaTime;
            if(despawnTime <= 0f)
            {
                Destroy(gameObject);
            }
        }
    }

    void Start()
    {
        //Rigidbody rb = GetComponent<Rigidbody>();
        //rb.constraints = RigidbodyConstraints.FreezeAll;
    }
}
