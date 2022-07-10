using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PersonNavMesh : MonoBehaviour
{
    protected NavMeshAgent nav;
    [Tooltip("All points that this object should move between in order")]
    public List<GameObject> destinations;
    [Tooltip("Current destination")]
    public GameObject currentDestination;
    [Tooltip("Current destination index")]
    public int currDestIndex = 0;
    [Tooltip("the distance between the agent and destination when the next destination is set")]
    public float destDist = .5f;
    [Tooltip("if it has arrived at a goal")]
    public bool arrived;
    [Tooltip("prev location")]
    private Vector3 prevLoc;

    [Tooltip("dust for the left wheel")]
    public ParticleSystem leftSideDust;
    [Tooltip("dust for the right wheel")]
    public ParticleSystem rightSideDust;
    // Start is called before the first frame update
    public virtual void Start()
    {
        nav = gameObject.GetComponent<NavMeshAgent>();
        currentDestination = destinations[currDestIndex];
        prevLoc = transform.position;
    }

    // Update is called once per frame
    public virtual void FixedUpdate()
    {
        if (GameManager.instance.IsPaused())
        {
            nav.destination = transform.position;
            return;
        }
        nav.destination = currentDestination.transform.position;


        if(((transform.position - prevLoc).magnitude) > 0.03f)
        //if(transform.velocity )
        {
            leftSideDust.Play();
            rightSideDust.Play();
        }
        else
        {
            leftSideDust.Stop();
            rightSideDust.Stop();
        }
        //float distance = (transform.position - currentDestination.transform.position).magnitude;
        //Debug.Log(distance);

        //if(distance < destDist)
        //{
        //    Debug.Log("Success");
        //    //    Invoke("SetNextDestination", currentDestination.GetComponent<Destination>().GetWaitTime());
        //}
        prevLoc = transform.position;
    }

    public virtual void SetNextDestination()
    {
        currDestIndex = (currDestIndex + 1) % destinations.Count;
        currentDestination = destinations[currDestIndex];
        nav.destination = currentDestination.transform.position;
        arrived = false;
    }

    public virtual void OnTriggerEnter(Collider col)
    {
        Debug.Log("collision");

        //foreach (ContactPoint contact in col.contacts)
        //{
        //    Debug.Log(vehicleLayer);
            if (col.gameObject == currentDestination)
            {
                Debug.Log("Success");
                Invoke("SetNextDestination", currentDestination.GetComponent<Destination>().GetWaitTime());
                arrived = true;
            }
        //}
    }
}
