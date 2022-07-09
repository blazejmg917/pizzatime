using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PersonNavMesh : MonoBehaviour
{
    private NavMeshAgent nav;
    // Start is called before the first frame update
    void Start()
    {
        nav = gameObject.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
