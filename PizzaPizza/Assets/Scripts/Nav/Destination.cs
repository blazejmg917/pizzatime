using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destination : MonoBehaviour
{
    [Tooltip("How long a navmesh should wait here before moving on to the next destination")]
    public float waitTime;
    public float GetWaitTime()
    {
        return waitTime;
    }
}
