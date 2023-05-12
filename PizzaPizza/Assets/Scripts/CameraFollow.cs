using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    //contains the different types of camera movement
    public enum CameraType { StaticFollow, DynamicFollow, StandardMoving, DynamicMoving };

    [Tooltip("the car to follow")]
    public GameObject car;
    [Tooltip("the camera's forward offset from the vehicle")]
    public float forwardOffset;
    [Tooltip("the camera's upwards offset from the vehicle")]
    public float upOffset;
    [Tooltip("the car's rb for rotation purposes")]
    public Rigidbody rb;
    [Tooltip("desired position")]
    public Vector3 pos;
    [Tooltip("catchup speed")]
    public float catchup = 5f;

    [Header("stats for different camera follow types")]
    [Tooltip("the current camera follow type")]
    public CameraType cameraType = CameraType.StaticFollow;
    [Space(2)]

    [Header("stats for static follow camera")]
    [Tooltip("Boost delay")]
    public float boostDelay = .05f;
    public float boostTimer = 0f;
    public float fovStartChange = 1.03f;
    public float fovEndChange = 1.01f;
    public float startingFOV;
    [Space(2)]
    [Header("stats for dynamic follow camera")]
    [Tooltip("the range of difference that the car's standard speed can make \n" +
        "positive zooms out when moving faster, negative zooms in when moving faster")]
    public float shiftMagnitude = .8f;

    [Header("stats for standard moving camera")]
    [Tooltip("the max speed the camera can move")]
    public float cameraSpeed;
    [Tooltip("whether the camera should use it's base offset to determine it's max dist. " +
        "\n Yes: calculates it as a percentage of its starting offset using maxDistPercent. " +
        "\n No: uses maxDist as the max distance")]
    public bool useMaxDistPercent = true;
    [Tooltip("the percentage of it's normal offset that is the maximum distance the camera can move from its base location. \n 1 = starting offset, 2 = double starting offset, etc.")]
    public float maxDistPercent = 3f;
    [Tooltip("the max distance the camera can move from its base location")]
    public float maxDist = 10;
    //[Tooltip("magnitude for the camera shake")]
    //public float shakeMag = .05f;
    //private Vector3 prevShake = Vector3.zero;



    //delegate type to hold camera movement functions for different movement types
    private delegate void cameraFollowFunction(Vector3 baseLocation);
    //array of delegates to hold all different camera types
    private cameraFollowFunction[] cameraFollow;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = car.transform.position + car.transform.forward * forwardOffset + car.transform.up * upOffset;
        startingFOV = Camera.main.fieldOfView;
        cameraFollow = new cameraFollowFunction[] { StaticFollow, DynamicFollow, StandardMoving, DynamicMoving };
        maxDist = Mathf.Sqrt((forwardOffset * forwardOffset) + (upOffset * upOffset)) * maxDistPercent;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //get the base location behind the car that the camera will work around
        Vector3 baseLocation = car.transform.position + car.GetComponent<VehicleMovement>().GetHeading() * forwardOffset + Vector3.up * upOffset;
        //call the camera follow function for the current camera type
        cameraFollow[(int)cameraType](baseLocation);
        //get the difference between the camera and car positions
        Vector3 diff = car.transform.position - transform.position;
        transform.rotation = Quaternion.LookRotation(car.GetComponent<VehicleMovement>().GetHeading());
        //Debug.Log(transform.rotation);
        transform.rotation = Quaternion.LookRotation(new Vector3(diff.x, 0, diff.z), Vector3.up);
        //transform.rotation = rb.transform.rotation;
    }

    public void StaticFollow(Vector3 baseLocation)
    {
        if (boostTimer > 0f)
        {
            boostTimer -= Time.fixedDeltaTime;
            Camera.main.fieldOfView *= fovStartChange;
        }
        else if (Camera.main.fieldOfView > startingFOV)
        {
            if (fovEndChange > (Camera.main.fieldOfView / startingFOV))
            {
                Camera.main.fieldOfView = startingFOV;
            }
            else
            {
                Camera.main.fieldOfView /= fovEndChange;
            }
        }
        else if (Camera.main.fieldOfView < startingFOV)
        {
            Camera.main.fieldOfView = startingFOV;

        }
        transform.position = baseLocation;
    }

    public void DynamicFollow(Vector3 baseLocation)
    {
        Vector3 diff = car.transform.position - baseLocation;
        diff = diff.normalized * (diff.magnitude + (car.GetComponent<VehicleMovement>().GetNormalSpeed() * shiftMagnitude));
        transform.position = car.transform.position - diff;
    }

    public void StandardMoving(Vector3 baseLocation)
    {
        //transform.position -= prevShake;
        transform.position = new Vector3(transform.position.x, baseLocation.y, transform.position.z);
        Vector3 moveDiff = baseLocation - transform.position;
        if (moveDiff.magnitude < cameraSpeed)
        {
            transform.position = baseLocation;
        }
        else {
            transform.position = transform.position + moveDiff.normalized * cameraSpeed;
        }
        moveDiff = baseLocation - transform.position;
        //Debug.Log("dist: " + moveDiff.magnitude);
        if (moveDiff.magnitude > maxDist)
        {
            transform.position = baseLocation - (moveDiff.normalized* maxDist);
            //prevShake = GetShake();
            //transform.position += prevShake;
        }
    }

    public void DynamicMoving(Vector3 baseLocation)
    {
        Vector3 diff = car.transform.position - baseLocation;
        diff = diff.normalized * (diff.magnitude + (car.GetComponent<VehicleMovement>().GetNormalSpeed() * shiftMagnitude));
        StandardMoving(car.transform.position - diff);
    }

    public void ResetCamera()
    {
        transform.position = car.transform.position + car.GetComponent<VehicleMovement>().GetHeading() * forwardOffset + Vector3.up * upOffset;
    }

    public void Boost()
    {
        boostTimer = boostDelay;
    }

    //private Vector3 GetShake()
    //{
    //    float x = Random.Range(-1f, 1f) * shakeMag;
     //   float y = Random.Range(-1f, 1f) * shakeMag;
     //   return (transform.right * x) + (transform.up * y);
    //}

}
