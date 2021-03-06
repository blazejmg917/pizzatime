using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Runtime.CompilerServices;

public class VehicleMovement : MonoBehaviour
{
    //the input system
    private NewControls inputs;
    //the acceleration/decceleration from input
    public float speed;
    //the turning from input
    public float turn;
    [Tooltip("the vehicle's max speed")]
    public float maxSpeed;
    //[Tooltip("the vehicle's min speed. 0 if can't back up, otherwise make it negative.")]
    //public float minSpeed;
    [Tooltip("the vehicle's max forward acceleration per frame")]
    public float forwardAcceleration;
    [Tooltip("the vehicle's max backward acceleration per frame")]
    public float backwardAcceleration;
    [Tooltip("the vehicle's max turn speed per frame while moving")]
    public float movingTurnSpeed;
    [Tooltip("the vehicle's max turn speed per frame while not moving")]
    public float standingTurnSpeed;
    //the vehicle's rigidbody to follow
    public Rigidbody rb;
    [Tooltip("how quickly the car tries to turn itself upright")]
    public float rightingTurn;

    [Header("gravity/grounded variables")]
    [Tooltip("gravity on the car")]
    public float gravityMagnitude;
    [Tooltip("If the car is grounded or not")]
    private bool grounded;
    [Tooltip("The layer(s) which constitute as ground")]
    public LayerMask groundLayer;
    [Tooltip("the length of the ground ray layer")]
    public float groundRayLength;
    [Tooltip("The point where the raycast is cast from")]
    public Transform groundRayPoint;
    [Tooltip("grounded drag")]
    public float groundDrag = 3f;
    [Tooltip("aerial drag")]
    public float airDrag = .1f;
    [Tooltip("the rate at which the car tilts forward when in the air. Should be small")]
    public float airtilt = 5;


    //previous Quaternion heading. used for aerial camera.
    public Vector3 prevHeading;
    // Start is called before the first frame update
    void Start()
    {
        //set up inputs
        inputs = new NewControls();
        inputs.Vehicle.AccelerateBrake.performed += ctx => speed = ctx.ReadValue<float>();
        
        inputs.Vehicle.AccelerateBrake.canceled += ctx => speed = 0;
        inputs.Vehicle.Turn.performed += ctx => turn = ctx.ReadValue<float>();
        inputs.Vehicle.Turn.canceled += ctx => turn = 0;
        //get rigidbody
        //rb = gameObject.GetComponent<Rigidbody>();
        rb.transform.parent = null;

        prevHeading = new Vector3(transform.forward.x, 0, transform.forward.z);
    }

    //get good stuff
    public void setSpeed(InputAction.CallbackContext newSpeed)
    {
        
        speed = newSpeed.ReadValue<float>();
        //Debug.Log("speed " + speed);
    }

    //get good stuff
    public void setTurn(InputAction.CallbackContext newTurn)
    {
        //Debug.Log("turn");
        turn = newTurn.ReadValue<float>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        transform.position = rb.transform.position;

        grounded = false;
        RaycastHit hit;
        if (Physics.Raycast(groundRayPoint.position, -Vector3.up, out hit, groundRayLength, groundLayer))
        {
            grounded = true;
            //if(Vector3.Angle(transform.rotation,hit.normal > 160))
            //transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
            transform.rotation = Quaternion.LookRotation(Vector3.Cross(transform.right, hit.normal));
        }

        Debug.Log(grounded);


        //update speed with input and friction
        //Debug.Log("speed " + speed + " mag " + rb.velocity.magnitude + " acc " + forwardAcceleration * 1000f);
        //float currSpeed = rb.velocity.magnitude;
        //if(Mathf.Abs(currSpeed) > 0f && Vector3.Angle(rb.velocity, transform.forward) > 90)
        //{
        //    currSpeed *= -1;
        //}
        //float newSpeed = Mathf.Clamp(currSpeed + (speed * forwardAcceleration), -maxSpeed, maxSpeed);
        if (grounded)
        {
            prevHeading = new Vector3(transform.forward.x, 0, transform.forward.z);
            rb.drag = groundDrag;
            if (speed > 0)
            {
                rb.AddForce(transform.forward * speed * forwardAcceleration * 1000f);
            }
            else
            {
                rb.AddForce(transform.forward * speed * backwardAcceleration * 1000f);
            }
        }
        else
        {
            rb.drag = airDrag;
            rb.AddForce(-Vector3.up * gravityMagnitude * 100f);
        }
        
        //float friction = GetFriction();
        //if(friction > Mathf.Abs(rb.velocity))
        //{
        //    friction = rb.velocity;
        //}
        //friction *= -Mathf.Sign(speed);

        //turn car with input
        if (Mathf.Abs(turn) > 0f)
        {
            Vector3 upLine = transform.up;
            if (!grounded)
            {
                upLine = Vector3.up;
            }
            float speedFactor = speed;
            if (!grounded)
            {
                speedFactor = 1;
            }
            if (Mathf.Abs(speedFactor) > 0f)
            {
                transform.RotateAround(transform.position, upLine, turn * movingTurnSpeed * speedFactor);
            }
            else
            {
                transform.RotateAround(transform.position, upLine, turn * standingTurnSpeed);
            }
            //rb.angularVelocity = Vector3.zero;

            //rb.velocity = transform.forward.normalized * newSpeed;
        }

        if (!grounded)
        {
            transform.RotateAround(transform.position, transform.right, airtilt * speed);
        }
        //apply extra gravity to car
        //rb.AddForce()
        
    }

    //gets the current ground friction that the vehicle is experiencing
    private float GetFriction()
    {
        return 0.1f;
    }

    //gets the rb
    private Rigidbody getRB()
    {
        return rb;
    }


    public Vector3 GetHeading()
    {
        //if (grounded)
        //{
        //return new Vector3(transform.forward.x, 0, transform.forward.z);
        //}
        //return prevHeading;
        return Vector3.Cross(transform.right, Vector3.up);
    }

}
