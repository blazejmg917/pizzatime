using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LooseObject : MonoBehaviour
{
    [Tooltip("The vehicle layer")]
    public string vehicleLayer;
    [Tooltip("The garlic wyrm layer")]
    public string wyrmLayer;
    [Tooltip("the velocity needed to knock down this object")]
    public float breakVelocity;
    [Tooltip("If the object is broken yet")]
    public bool broken;
    [Tooltip("if this object despawns or not")]
    public bool despawns;
    [Tooltip("how long it takes for this object to despawn if ever")]
    public float despawnTime;
    public float despawnTimer;
    [Tooltip("whether this object respawns or not")]
    public bool respawn;
    [Tooltip("the spot this object will respawn in")]
    public Vector3 respawnPoint;
    [Tooltip("the rotation this object will respawn in")]
    public Quaternion respawnRotation;

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
            if((contact.otherCollider.gameObject.layer == LayerMask.NameToLayer(vehicleLayer) 
                || contact.otherCollider.gameObject.layer == LayerMask.NameToLayer(wyrmLayer)) 
                && col.relativeVelocity.magnitude > breakVelocity)
            {
                Break();
            }
        }
            
    }

    public virtual void Break()
    {
        Debug.Log("player");
        broken = true;
        despawnTimer = despawnTime;
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.None;
    }

     void FixedUpdate()
    {
        if(broken && (despawns || respawn))
        {
            despawnTimer -= Time.fixedDeltaTime;
            if(despawnTimer <= 0f)
            {
                if (respawn)
                {
                    Respawn();
                    broken = false;
                }
                else
                {
                    Destroy(gameObject);
                }
                
            }
        }
    }

    public virtual void Respawn()
    {
        transform.position = respawnPoint;
        transform.rotation = respawnRotation;
        Destroy(gameObject.GetComponent<Rigidbody>());
        despawnTimer = despawnTime;
    }

    void Start()
    {
        despawnTimer = despawnTime;
        //Rigidbody rb = GetComponent<Rigidbody>();
        //rb.constraints = RigidbodyConstraints.FreezeAll;
        if (respawn)
        {
            SetRespawnPoint(transform.position);
            SetRespawnRotation(transform.rotation);
        }
    }

    public bool IsBroken()
    {
        return broken;
    }

    public void SetRespawnPoint(Vector3 pos)
    {
        respawnPoint = pos;
    }

    public void SetRespawnRotation(Quaternion rot)
    {
        respawnRotation = rot;
    }
}
