using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Tooltip("the car to follow")]
    public GameObject car;
    [Tooltip("the camera's forward offset from the vehicle")]
    public float forwardOffset;
    [Tooltip("the camera's upwards offset from the vehicle")]
    public float upOffset;
    [Tooltip("the car's rb for rotation purposes")]
    public Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = car.transform.position + car.transform.forward * forwardOffset + car.transform.up * upOffset;  
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = car.transform.position + car.GetComponent<VehicleMovement>().GetHeading() * forwardOffset + Vector3.up * upOffset;
        Vector3 diff = car.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(car.GetComponent<VehicleMovement>().GetHeading());
        //Debug.Log(transform.rotation);
        transform.rotation = Quaternion.LookRotation(new Vector3(diff.x, 0, diff.z), Vector3.up);
        //transform.rotation = rb.transform.rotation;
    }

}
