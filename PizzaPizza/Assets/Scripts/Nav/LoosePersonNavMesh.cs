using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoosePersonNavMesh : PersonNavMesh
{
    public LooseObject looseScript;
    // Start is called before the first frame update
    public override void Start()
    {
        looseScript = gameObject.GetComponent<LooseObject>();
        base.Start();
    }

    public override void FixedUpdate()
    {
        if (GameManager.instance.IsPaused())
        {
            nav.destination = transform.position;
            return;
        }
        if (!looseScript.IsBroken())
        {
            base.FixedUpdate();
        }
        else
        {
            looseScript.SetRespawnPoint(transform.position);
            nav.destination = transform.position;
        }
    }

    public override void SetNextDestination()
    {
        if (!looseScript.IsBroken())
        {
            base.SetNextDestination();
        }
    }

    public override void OnTriggerEnter(Collider col)
    {
        if (!looseScript.IsBroken())
        {
            base.OnTriggerEnter(col);
        }
    }
}
